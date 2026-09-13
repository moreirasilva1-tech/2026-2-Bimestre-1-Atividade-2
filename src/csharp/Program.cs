using System;

namespace AtividadeThreads
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("    INICIANDO EXECUÇÃO DAS THREADS      ");
            Console.WriteLine("========================================\n");

            Console.WriteLine("--- Executando Exercício 1 ---");
            ExercicioThread1.Executar();

            Console.WriteLine("\n--- Executando Exercício 2 ---");
            ExercicioThread2.Executar();

            Console.WriteLine("\n--- Executando Exercício 3 ---");
            ExercicioThread3.Executar();

            Console.WriteLine("\n========================================");
            Console.WriteLine(" Todas as threads foram concluídas com sucesso!");
            Console.WriteLine("========================================");
        }
    }
}