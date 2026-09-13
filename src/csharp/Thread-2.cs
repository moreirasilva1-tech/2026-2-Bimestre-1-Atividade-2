using System;
using System.Threading;

namespace AtividadeThreads
{
    public static class ExercicioThread2
    {
        // Função que recebe parâmetros (nome e quantidade de saudaçôes)
        static void Saudar(string nome, int vezes)
        {
            for (int i = 0; i < vezes; i++)
            {
                Console.WriteLine($"Olá, {nome}! (mensagem {i + 1})");
                Thread.Sleep(500); // Pequena pausa para simular processamento
            }
        }
        
        public static void Executar()
        {
            // Usamos uma expressão lambda () => para passar argumentos para função
            Thread thread = new Thread(() => Saudar("Maria", 3));

            thread.Start();
            thread.Join();
        }

    }
}