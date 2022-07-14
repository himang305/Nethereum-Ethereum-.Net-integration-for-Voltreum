using System;
using System.Threading.Tasks;
using Nethereum.Web3;
using Nethereum.Signer;
using Nethereum.Web3.Accounts;
using Nethereum.Util;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using Nethereum.Contracts.Extensions;
using System.Numerics;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NethereumSample
{
    class Program
    {

        //  ABI for add Bills function
        [Function("addbills")]
        public class AddBillsFunction : FunctionMessage
        {
            [Parameter("address[]", "_userAddress", 1)]
            public string[] Users { get; set; }

            [Parameter("uint256[]", "_bills", 2)]
            public BigInteger[] Bill { get; set; }
        }


        //  ABI for add payments function
        [Function("addpayments")]
        public class AddPaymentsFunction : FunctionMessage
        {
            [Parameter("address[]", "_userAddress", 1)]
            public string[] Users { get; set; }

            [Parameter("uint256[]", "_payments", 2)]
            public BigInteger[] Payment { get; set; }
        }


        static void Main(string[] args)
        {
            SubmitData().Wait();
            Console.ReadLine();
        }

        static async Task SubmitData()
        {
            var privateKey = "32f1385ea2afdc0243b0e57aee55c455e06a581158b72809e34a0e95262e5684";
            var chainId = 6796;
            var account = new Account(privateKey, chainId);
            Console.WriteLine("Our account: " + account.Address);

            var web3 = new Web3(account, "https://data-seed1.gdccoin.io");
            web3.TransactionManager.UseLegacyAsDefault = true;

            // Voltreum Core Contract
            var contractAddress = "0x5F8d48a8d4BEEd838a3fD9c3D5aC204b9E572E11";

            // Bills Generated for consumers
            string[] consumersAddress = { "0xf922e3223567AeB66e6986cb09068B1B879B6ccc", "0xf922e3223567AeB66e6986cb09068B1B879B6ccc" };
            BigInteger[] consumer_amounts = { 1000, 1000 };    // use uint

            // Data Generated for Producers
            string[] producersAddress = { "0xf922e3223567AeB66e6986cb09068B1B879B6ccc", "0xf922e3223567AeB66e6986cb09068B1B879B6ccc" };
            BigInteger[] producers_amounts = { 1000, 1000 };    // use uint

            var transferHandler = web3.Eth.GetContractTransactionHandler<AddBillsFunction>();
            var transferHandler2 = web3.Eth.GetContractTransactionHandler<AddPaymentsFunction>();

            var billsTransfer = new AddBillsFunction()
            {
                Users = consumersAddress,
                Bill = consumer_amounts
            };

            var paymentTransfer = new AddPaymentsFunction()
            {
                Users = producersAddress,
                Payment = producers_amounts
            };

            var transactionTransferReceipt =
            await transferHandler.SendRequestAndWaitForReceiptAsync(contractAddress, billsTransfer);
            Console.WriteLine("Transaction hash transfer is: " + transactionTransferReceipt.TransactionHash);

            var transactionTransferReceipt2 =
            await transferHandler2.SendRequestAndWaitForReceiptAsync(contractAddress, paymentTransfer);
            Console.WriteLine("Transaction hash transfer is: " + transactionTransferReceipt2.TransactionHash);

        }

    }
}
