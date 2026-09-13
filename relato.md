# Relatório de implementação de linha de execução em C#

## Introdução

Este relato faz parte do processo avaliativo da disciplina de Sistemas Operacionais no Curso Superior de Tecnologia em Análise e Desenvolvimento de Sistemas, ofertado pela Diretoria Acadêmica de Gestão e Tecnologia da Informação no Campus Natal-Central do Instituto Federal de Educação, Ciência e Tecnologia do Rio Grande do Norte.

Tem como objetivo principal relatar como implementar linhas de execução na linguagem C#.

O grupo de trabalho foi formado por:
Álvaro Luiz Barbalho de Souza Filho
Paulo Cesar Moreira da Silva
Pedro Messias Dias Neto

## Implementando múltiplas linhas de execução em C#

### Informações gerais sobre C#

C# é uma linguagem de programação de propósito geral desenvolvida pela Microsoft e atualmente utilizada em conjunto com a plataforma .NET. A linguagem possui como principal paradigma a orientação a objetos, mas também apresenta recursos de outros paradigmas, como programação funcional e programação concorrente.

Um dos recursos disponibilizados por C# para trabalhar com linhas de execução é a classe Thread, pertencente ao namespace System.Threading. Por meio dessa classe, é possível criar, iniciar e controlar linhas de execução dentro de um programa.

O objetivo da linguagem é possibilitar o desenvolvimento de diferentes tipos de aplicações, como sistemas desktop, aplicações web, jogos, aplicativos, serviços e outros softwares. No contexto deste trabalho, será utilizada para demonstrar o funcionamento de múltiplas linhas de execução e sua execução concorrente.

C# está disponível por meio da plataforma .NET, que pode ser utilizada em diferentes sistemas operacionais, como Windows, Linux e macOS. Dessa forma, aplicações desenvolvidas em C# podem ser executadas em diferentes ambientes que possuam suporte à plataforma .NET.

### Criando linhas de execução

Uma linha de execução pode ser criada em C# utilizando a classe Thread, presente no namespace System.Threading.

Para criar uma thread, primeiro é necessário definir uma função que será executada por ela. Depois, essa função é associada a um objeto Thread. A execução é iniciada utilizando o método Start().

O primeiro código demonstra esse funcionamento:
```csharp
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
            Console.WriteLine("Thread finalizada");
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
```

Nesse exemplo, o método MinhaFuncao() representa a tarefa que será executada pela nova thread. A instrução new Thread(MinhaFuncao) cria um objeto Thread associado ao método.

A thread não começa sua execução no momento em que é criada. Para iniciá-la, é necessário utilizar o método Start(). Depois disso, o sistema operacional pode escalonar essa thread para execução.

Dentro de MinhaFuncao(), o método Thread.Sleep(2000) interrompe temporariamente a execução da thread durante 2000 milissegundos, ou seja, dois segundos. Depois desse período, a execução continua e a mensagem "Thread finalizada!" é exibida.

O método Join() possui outra função importante. Ele faz com que a linha de execução que chamou esse método aguarde a conclusão da thread criada. Dessa forma, o método Executar() somente prossegue após a finalização da execução de MinhaFuncao().

Esse exemplo demonstra, portanto, os principais passos para trabalhar com uma thread em C#: criação, início da execução e espera pelo término.


### Passando valores para linhas de execução

Além de executar uma função, uma thread pode precisar receber informações para realizar sua tarefa. Em C#, uma forma de fazer isso é utilizar uma expressão lambda.

O segundo código demonstra essa possibilidade:
```csharp
using System;
using System.Threading;

namespace AtividadeThreads
{
    public static class ExercicioThread2
    {
        // Função que recebe parâmetros (nome e quantidade de saudações)
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
```
Nesse código, o método Saudar() recebe dois parâmetros: nome, que é uma variável do tipo string, e vezes, que é uma variável do tipo int.

A expressão () => Saudar("Maria", 3) é uma função lambda. Ela permite definir uma função que será executada pela thread e, nesse caso, permite passar os valores "Maria" e 3 para o método Saudar().

Quando a thread é iniciada com Start(), o método Saudar() é executado. O laço for faz com que a mensagem seja exibida três vezes:

```text
Olá, Maria! (mensagem 1)
Olá, Maria! (mensagem 2)
Olá, Maria! (mensagem 3)
```

O uso de parâmetros é importante porque permite que diferentes threads executem a mesma função utilizando informações diferentes. Dessa maneira, uma mesma tarefa pode ser reutilizada para diferentes situações.
### Múltiplas linhas de execução

O terceiro código apresenta uma situação mais próxima do uso de múltiplas linhas de execução. Nele, são criadas cinco threads, cada uma representando um trabalhador que executa uma tarefa.

O código utiliza também a classe Stopwatch, pertencente ao namespace System.Diagnostics, para medir o tempo total de execução.
```csharp
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

           // Array para armazenar as threads
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
```
Nesse programa, a função Trabalhador() recebe dois valores. O primeiro representa o número do trabalhador e o segundo representa o tempo necessário para realizar sua tarefa.

O comando:
```csharp
Thread.Sleep(tempoTrabalhoSegundos * 1000);
```

simula o tempo de execução da tarefa. Como o valor utilizado é 2, cada trabalhador permanece executando sua tarefa durante aproximadamente dois segundos.

A diferença principal em relação aos exemplos anteriores é que agora são criadas cinco threads. Para armazená-las, foi utilizado um array do tipo Thread:

```csharp
Thread[] threads = new Thread[5];
```

Em seguida, um laço for é utilizado para criar e iniciar cada uma das cinco threads:

```csharp
for (int i = 0; i < 5; i++)
{
    int idTrabalhador = i;
    threads[i] = new Thread(() => Trabalhador(idTrabalhador, 2));
    threads[i].Start();
}
```

Cada posição do array armazena uma thread diferente. A variável idTrabalhador recebe uma cópia do valor de i para que cada thread utilize corretamente seu próprio identificador ao executar a função Trabalhador().

Depois que todas as threads são iniciadas, o programa percorre o array utilizando foreach e chama Join() em cada uma delas:

```csharp
foreach (Thread t in threads)
{
    t.Join();
}
```
Isso garante que o programa principal aguarde todas as threads antes de continuar. Somente quando os cinco trabalhadores terminarem o cronômetro é interrompido e o tempo total é exibido.

Como cada trabalhador leva aproximadamente dois segundos e as tarefas são executadas concorrentemente, o tempo total tende a ficar próximo de dois segundos, em vez de aproximadamente dez segundos que seriam necessários caso os cinco trabalhadores executassem suas tarefas um depois do outro.

Essa diferença demonstra uma das vantagens da utilização de múltiplas linhas de execução. Enquanto uma execução sequencial realizaria uma tarefa e somente depois começaria a próxima, a utilização de threads permite que diferentes tarefas possam progredir de forma concorrente.

É importante destacar que a execução das threads é controlada pelo sistema operacional e pelo escalonador. Portanto, a ordem das mensagens exibidas no console pode variar entre diferentes execuções.

## Considerações finais

A partir dos exemplos apresentados, foi possível observar como a linguagem C# permite implementar e controlar linhas de execução utilizando a classe Thread.

O primeiro exemplo apresentou a criação de uma única thread, demonstrando os métodos Start() e Join(). O segundo mostrou como utilizar uma expressão lambda para passar valores para uma função executada por uma thread. Por fim, o terceiro exemplo demonstrou a criação de cinco threads, permitindo observar na prática a execução concorrente de diferentes tarefas.

A utilização de múltiplas linhas de execução é um recurso importante para o desenvolvimento de sistemas que precisam realizar diferentes tarefas de maneira concorrente. Além de possibilitar melhor aproveitamento dos recursos computacionais em determinadas situações, o conceito também é fundamental para compreender como os sistemas operacionais gerenciam processos e suas respectivas linhas de execução.

Dessa forma, os códigos apresentados permitem compreender, de maneira prática, conceitos relacionados à criação, execução, passagem de parâmetros, sincronização e concorrência entre threads em C#.