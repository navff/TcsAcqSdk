using TinkkoffAcquiringSdk;
using TinkkoffAcquiringSdk.Requests;
using TinkkoffAcquiringSdk.Responses;

namespace UnitTests
{
    public class AcquiringApiClientTests
    {
        private readonly AcquiringApiClient _client;

        public AcquiringApiClientTests()
        {
            AcquiringApiClient.IsDeveloperMode = true;
            _client = new AcquiringApiClient(TestPaymentData.TestTerminalKey, TestPaymentData.TestPassword);
        }

        private async Task<long?> InitSessionAsync(bool isRecurrent)
        {
            var request = new InitRequest
            {
                Amount = 2000,
                OrderId = new Random().Next().ToString(),
                ChargeFlag = isRecurrent,
                CustomerKey = TestPaymentData.TestCustomerKey,
                PayForm = TestPaymentData.TestPayForm,
                Recurrent = isRecurrent,
                SuccessURL = "https://test.anyor.ru",
                FailURL = "https://test.anyor.ru/payment/fail"
            };
            var response = await _client.InitPaymentSessionAsync(request);

            var paymentId = response.PaymentId;

            return paymentId;
        }


        [Fact]
        public async Task Get_a_valid_state_after_starting_a_payment_session()
        {
            var paymentId = await InitSessionAsync(false);
            var request = new GetStateRequest(paymentId.Value);

            var response = await _client.GetStateAsync(request);

            Assert.True(response.IsSuccess && response.GetStatusType() == ResponseStatusType.New);
        }
    }
}