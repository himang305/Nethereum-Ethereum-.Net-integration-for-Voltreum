// using System;
// using System.Threading.Tasks;
// using Nethereum.Web3;
// using Nethereum.Signer; 
// using Nethereum.Web3.Accounts; 
// using Nethereum.Util; 
// using Nethereum.Hex.HexConvertors.Extensions; 
// using Nethereum.RPC.Eth.DTOs; 
// using Nethereum.Hex.HexTypes;
// using Nethereum.ABI.FunctionEncoding.Attributes;
// using Nethereum.Contracts.CQS;
// using Nethereum.Contracts;
// using Nethereum.Contracts.Extensions;
// using System.Numerics;
// using System;
// using System.Threading;
// using System.Threading.Tasks;

// namespace NethereumSample
// {
//     class Program
//     {

// 	[Function("balanceOf", "uint256")]
// 	public class BalanceOfFunction : FunctionMessage
// 	{
// 		[Parameter("address", "_owner", 1)]
// 		public string Owner { get; set; }
// 	}

//     	[Function("transfer", "bool")]
// 	public class TransferFunction : FunctionMessage
// 	{
// 		[Parameter("address", "_to", 1)]
// 		public string To { get; set; }

// 		[Parameter("uint256", "_value", 2)]
// 		public BigInteger TokenAmount { get; set; }
// 	}

//         static void Main(string[] args)
//         {
//             GetAccountBalance().Wait();
//             Console.ReadLine();
//         }

//         static async Task GetAccountBalance()
//         {
//         var privateKey = "32f1385ea2afdc0243b0e57aee55c455e06a581158b72809e34a0e95262e5684";
// 		var chainId = 6796; //Nethereum test chain, chainId
// 		var account = new Account(privateKey, chainId);
// 		Console.WriteLine("Our account: " + account.Address);

// 		//Now let's create an instance of Web3 using our account pointing to our nethereum testchain
// 		var web3 = new Web3(account, "https://data-seed1.gdccoin.io");
// 		web3.TransactionManager.UseLegacyAsDefault = true; //Using legacy option instead of 1559
	
// 		// *** INTERACTING WITH THE CONTRACT

// 		// To retrieve the balance, we will create a QueryHandler and finally using our contract address 
// 		// and message retrieve the balance amount.

// 		var balanceOfFunctionMessage = new BalanceOfFunction()
// 		{
// 			Owner = account.Address,
// 		};
// 		var contractAddress = "0x1FaBA667615fdbb5e9C0b37b977285152Efc451b";

// 		var balanceHandler = web3.Eth.GetContractQueryHandler<BalanceOfFunction>();

// 		var balance = await balanceHandler.QueryAsync<BigInteger>(contractAddress, balanceOfFunctionMessage);

// 		Console.WriteLine("Balance of deployment owner address: " + balance);	



//        	var receiverAddress = "0xf922e3223567AeB66e6986cb09068B1B879B6ccc";
// 		var transferHandler = web3.Eth.GetContractTransactionHandler<TransferFunction>();

// 		var transfer = new TransferFunction()
// 		{
// 			To = receiverAddress,
// 			TokenAmount = 100
// 		};

// 		var transactionTransferReceipt =
// 			await transferHandler.SendRequestAndWaitForReceiptAsync(contractAddress, transfer);
// 		Console.WriteLine("Transaction hash transfer is: " + transactionTransferReceipt.TransactionHash);

// 		balance = await balanceHandler.QueryAsync<BigInteger>(contractAddress, balanceOfFunctionMessage);

// 		Console.WriteLine("Balance of deployment owner address after transfer: " + balance);


//         }

//     }
// }