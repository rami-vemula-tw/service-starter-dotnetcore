namespace PaymentApi.Messages{

    public class PaymentRequest {
        public int amount{get; set;}
        public PartyDetails beneficiary{get; set;}
        public PartyDetails payee{get; set;}
    }
}
