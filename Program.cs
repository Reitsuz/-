using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        List<Transaction> transactions = LoadTransactionsFromFile("transactions.txt");

        while (true)
        {
            Console.WriteLine("1. �������L�^����");
            Console.WriteLine("2. �x�o���L�^����");
            Console.WriteLine("3. �S�Ă̎����\������");
            Console.WriteLine("4. �I������");
            Console.Write("�I�����Ă�������: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    RecordIncome(transactions);
                    break;
                case 2:
                    RecordExpense(transactions);
                    break;
                case 3:
                    ShowTransactions(transactions);
                    break;
                case 4:
                    SaveTransactionsToFile(transactions, "transactions.txt");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("�����ȑI���ł��B");
                    break;
            }
        }
    }

    static void RecordIncome(List<Transaction> transactions)
    {
        Console.Write("�����̋��z����͂��Ă�������: ");
        decimal amount = decimal.Parse(Console.ReadLine());
        transactions.Add(new Transaction(amount, TransactionType.Income));
        Console.WriteLine("�������L�^����܂����B");
    }

    static void RecordExpense(List<Transaction> transactions)
    {
        Console.Write("�x�o�̋��z����͂��Ă�������: ");
        decimal amount = decimal.Parse(Console.ReadLine());
        transactions.Add(new Transaction(amount, TransactionType.Expense));
        Console.WriteLine("�x�o���L�^����܂����B");
    }

    static void ShowTransactions(List<Transaction> transactions)
    {
        if (transactions.Count == 0)
        {
            Console.WriteLine("�܂����������܂���B");
            return;
        }

        Console.WriteLine("����ꗗ:");
        foreach (Transaction transaction in transactions)
        {
            Console.WriteLine(transaction);
        }
    }

    static List<Transaction> LoadTransactionsFromFile(string filename)
    {
        List<Transaction> transactions = new List<Transaction>();

        if (File.Exists(filename))
        {
            string[] lines = File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                decimal amount = decimal.Parse(parts[0]);
                TransactionType type = (TransactionType)Enum.Parse(typeof(TransactionType), parts[1]);
                transactions.Add(new Transaction(amount, type));
            }
        }

        return transactions;
    }

    static void SaveTransactionsToFile(List<Transaction> transactions, string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (Transaction transaction in transactions)
            {
                writer.WriteLine($"{transaction.Amount},{transaction.Type}");
            }
        }
    }
}

enum TransactionType
{
    Income,
    Expense
}

class Transaction
{
    public decimal Amount { get; }
    public TransactionType Type { get; }

    public Transaction(decimal amount, TransactionType type)
    {
        Amount = amount;
        Type = type;
    }

    public override string ToString()
    {
        string typeString = Type == TransactionType.Income ? "����" : "�x�o";
        return $"{typeString}: {Amount}";
    }
}