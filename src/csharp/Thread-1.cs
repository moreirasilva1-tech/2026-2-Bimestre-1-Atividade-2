using System;
using System.Threading;

namespace AtividadeThreads
{
    class ExercicioThread1
    {
        //Função que será executada na thread secundária
        static void MinhaFuncao()
        {
            Console.WriteLine("Thread iniciada!");
            Thread.Sleep(2000); // Aguarda 2 segundos (2000 ms)
            Console.WriteLine("Thread finaizada");
        }

        public static void Executar()
        {
            // Instancia a thread apontando para função
            Thread thread = new Thread(MinhaFuncao);

            //Solicita ao escalonador do SO para iniciar a thread
            thread.Start();

            // Aguarda a thread finalizar antes de encerrar o programa principal
            thread.Join();
        }
    }
}