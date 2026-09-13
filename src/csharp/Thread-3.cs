using System;
using System.Diagnostics;
using System.Threading;

namespace AtividadeThreads
{
    public static class ExercicioThread3
    {
        public static void Executar()
        {
            Console.WriteLine("Iniciando 5 trabalhadores...");
            
            // Usamos Stopwatch no C# para medir o tempo exato (igual ao time.time do Python)
            Stopwatch stopwatch = Stopwatch.StartNew();

            // Lista para guardar as threads
            Thread[] threads = new Thread[5];

            // Criar e iniciar as 5 threads empilhadas
            for (int i = 0; i < 5; i++)
            {
                int idTrabalhador = i; // Copia a variável para evitar problema de escopo na lambda
                threads[i] = new Thread(() => Trabalhador(idTrabalhador, 2));
                threads[i].Start();
            }

            // Aguardar todas as threads terminarem (.join())
            foreach (Thread t in threads)
            {
                t.Join();
            }

            stopwatch.Stop();
            double tempoTotal = stopwatch.Elapsed.TotalSeconds;

            Console.WriteLine("\nTodos os trabalhadores terminaram!");
            Console.WriteLine($"Tempo total: {tempoTotal:F2}s");
            Console.WriteLine("(Se fosse sequencial, levaria ~10s)");
        }

        private static void Trabalhador(int numero, int tempoTrabalhoSegundos)
        {
            Console.WriteLine($"Trabalhador {numero} começou");
            Thread.Sleep(tempoTrabalhoSegundos * 1000); // C# usa milissegundos
            Console.WriteLine($"Trabalhador {numero} terminou (levou {tempoTrabalhoSegundos}s)");
        }
    }
}