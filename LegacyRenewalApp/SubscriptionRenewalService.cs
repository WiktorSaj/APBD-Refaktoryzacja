using System;

namespace LegacyRenewalApp
{
    public class SubscriptionRenewalService
    {
        
        private InputValidator _validator;
        private RenewalInputNormalizer _normalizer;
        private DiscountCalculator _discountCalculator;
        private PremiumSupportFeeCalculator _premiumSupportFeeCalculator;
        private PaymentFeeCalculator _paymentFeeCalculator;
        private TaxFeeCalculator _taxFeeCalculator;
        private IBillingGateway _billingGateway;
        private RenewalInvoiceBuilder _builder;


        public SubscriptionRenewalService(InputValidator validator,
            RenewalInputNormalizer normalizer,
            DiscountCalculator discountCalculator,
            PremiumSupportFeeCalculator premiumSupportFeeCalculator,
            PaymentFeeCalculator paymentFeeCalculator,
            TaxFeeCalculator taxFeeCalculator,
            IBillingGateway billingGateway,
            RenewalInvoiceBuilder builder)
        {
            _validator = validator;
            _normalizer = normalizer;
            _discountCalculator = discountCalculator;
            _premiumSupportFeeCalculator = premiumSupportFeeCalculator;
            _paymentFeeCalculator = paymentFeeCalculator;
            _taxFeeCalculator = taxFeeCalculator;
            _billingGateway = billingGateway;
            _builder = builder;
        }
        
        public SubscriptionRenewalService() : this(
            new InputValidator(),
            new RenewalInputNormalizer(),
            defaultDiscountCalculator(),
            defaultPremiumSupportFeeCalculator(),
            defaultPaymentFeeCalculator(),
            defaultTaxFeeCalculator(),
            new LegacyBillingGatewayAdapter(),
            new RenewalInvoiceBuilder()){}
        
        
        public RenewalInvoice CreateRenewalInvoice(
            int customerId,
            string planCode,
            int seatCount,
            string paymentMethod,
            bool includePremiumSupport,
            bool useLoyaltyPoints)
        {
            _validator.Validate(customerId, planCode, seatCount, paymentMethod);

            var normalizedInput = _normalizer.Normalize(planCode, paymentMethod);
            var normalizedPlanCode = normalizedInput.PlanCode;
            var normalizedPaymentMethod = normalizedInput.PaymentMethod;

            var (customer, plan) = GetData(customerId, normalizedPlanCode);

            

            if (!customer.IsActive)
            {
                throw new InvalidOperationException("Inactive customers cannot renew subscriptions");
            }

            decimal baseAmount = (plan.MonthlyPricePerSeat * seatCount * 12m) + plan.SetupFee;
            decimal discountAmount;
            string notes;
            
            var discountInto = _discountCalculator.Calculate(
                customer, plan, seatCount, baseAmount, useLoyaltyPoints);
            discountAmount = discountInto.DiscountAmount;
            decimal subtotalAfterDiscount = discountInto.TotalAfterDiscount;
            notes = discountInto.Notes;

            decimal supportFee = _premiumSupportFeeCalculator.Calculate(normalizedPlanCode, includePremiumSupport);
            
            if(includePremiumSupport) notes+= "premium support included; ";
            

            var paymentFeeInfo = _paymentFeeCalculator.CalculateFee(
                normalizedPaymentMethod, subtotalAfterDiscount, supportFee);
            
            decimal paymentFee = paymentFeeInfo.PaymentFee;
            notes += paymentFeeInfo.Note;
            
            decimal taxRate = _taxFeeCalculator.CalculateTax(customer.Country);
            decimal taxBase = subtotalAfterDiscount + supportFee + paymentFee;
            decimal taxAmount = taxBase * taxRate;
            decimal finalAmount = taxBase + taxAmount;

            if (finalAmount < 500m)
            {
                finalAmount = 500m;
                notes += "minimum invoice amount applied; ";
            }

            var invoice = _builder.Build(
                customerId, seatCount, normalizedPlanCode, customer, normalizedPaymentMethod, baseAmount,
                discountAmount, supportFee, paymentFee, taxAmount, finalAmount, notes);

            _billingGateway.SaveInvoice(invoice);

            if (!string.IsNullOrWhiteSpace(customer.Email))
            {
                string subject = "Subscription renewal invoice";
                string body =
                    $"Hello {customer.FullName}, your renewal for plan {normalizedPlanCode} " +
                    $"has been prepared. Final amount: {invoice.FinalAmount:F2}.";

                _billingGateway.SendEmail(customer.Email, subject, body);
            }

            return invoice;
        }

      


        private static (Customer customer, SubscriptionPlan subscriptionPlan) GetData(int customerId, string normalizedPlanCode)
        {
            var customerRepository = new CustomerRepository();
            var planRepository = new SubscriptionPlanRepository();

            var customer = customerRepository.GetById(customerId);
            var plan = planRepository.GetByCode(normalizedPlanCode);
            
            return (customer, plan);
        }


        private static DiscountCalculator defaultDiscountCalculator()
        {
            var segmentDiscountCalculator = new SegmentDiscountCalculator(new ISegmentDiscountStrategy[]
            {
                new SilverDiscount(),
                new GoldDiscount(),
                new PlatinumDiscount(),
                new EducationDiscount(),
            });

            var loyalityDiscountCalculator = new LoyalityDiscountCalculator(new ILoyalityDiscountStrategy[]
            {
                new LongTermDiscount(),
                new BasicLoyaltyDiscount()
            });

            var seatDiscountCalculator = new SeatDiscountCalculator(new ISeatDiscountStrategy[]
            {
                new LargeTeamDiscount(),
                new MediumTeamDiscount(),
                new SmallTeamDiscount()
            });
            
            return new DiscountCalculator(segmentDiscountCalculator, loyalityDiscountCalculator, seatDiscountCalculator);
        }


        private static PremiumSupportFeeCalculator defaultPremiumSupportFeeCalculator()
        {
            return new PremiumSupportFeeCalculator(new IPremiumSupportFeeStrategy[]
            {
                
                new EnterprisePremiumSupportFee(),
                new ProPremiumSupportFee(),
                new StartPremiumSupportFee()

            });
        }

        private static PaymentFeeCalculator defaultPaymentFeeCalculator()
        {
            return new PaymentFeeCalculator(new IPaymentFeeStrategy[]
            {

                new CardPaymentFee(),
                new BankTransferPaymentFee(),
                new PayPalPaymentFee(),
                new InvoicePaymentFee()
            });
        }

        private static TaxFeeCalculator defaultTaxFeeCalculator()
        {
            return new TaxFeeCalculator(new ITaxStrategy[]
            {

                new PolandTaxFee(),
                new GermanyTaxFee(),
                new CzechRepublicTaxFee(),
                new NorwayTaxFee()
            });
        }

    }
}
