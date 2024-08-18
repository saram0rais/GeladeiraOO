using System;
using System.Collections.Generic;

//Agora iremos aprimorar nosso sistema da geladeira! Vamos transformar nossa geladeira em orientação a objetos.

namespace GeladeiraOOP
{
    class Posicao
    {

        public string Produto { get; private set; }

        public Posicao(string produto = null)
        {
            Produto = produto;
        }

        public bool EstaVazia()
        {
            return Produto == null;
        }

        //Adicionar produto na geladeira.
        public void AdicionarProduto(string produto)
        {
            if (EstaVazia())
            {
                Produto = produto;
                Console.WriteLine("Produto adicionado com sucesso.");
            }
            else
            {
                Console.WriteLine("A posição já está ocupada.");
            }
        }

        //Remover produto da geladeira.
        public void RemoverProduto()
        {
            if (!EstaVazia())
            {
                Produto = null;
                Console.WriteLine("Produto removido com sucesso.");
            }
            else
            {
                Console.WriteLine("A posição já está vazia.");
            }
        }

        public override string ToString()
        {
            return EstaVazia() ? "Vazio" : Produto;
        }
    }

    //---------------------------VERIFICAÇÃO POR CONTAINER-----------------------------------
    class Container
    {
        private List<Posicao> Posicoes { get; set; }

        public Container(List<string> produtos)
        {
            Posicoes = new List<Posicao>();
            foreach (var produto in produtos)
            {
                Posicoes.Add(new Posicao(produto));
            }
        }

        public void AdicionarItem(int index, string produto)
        {
            if (index >= 0 && index < Posicoes.Count)
            {
                Posicoes[index].AdicionarProduto(produto);
            }
            else
            {
                Console.WriteLine("Índice de posição inválido.");
            }
        }

        public void RemoverItem(int index)
        {
            if (index >= 0 && index < Posicoes.Count)
            {
                Posicoes[index].RemoverProduto();
            }
            else
            {
                Console.WriteLine("Índice de posição inválido.");
            }
        }

        public void AdicionarItens(List<string> produtos)
        {
            foreach (var produto in produtos)
            {
                foreach (var posicao in Posicoes)
                {
                    if (posicao.EstaVazia())
                    {
                        posicao.AdicionarProduto(produto);
                        break;
                    }
                }
            }
        }

        public void RemoverTodosItens()
        {
            foreach (var posicao in Posicoes)
            {
                posicao.RemoverProduto();
            }
        }

        public override string ToString()
        {
            string conteudo = "";
            for (int i = 0; i < Posicoes.Count; i++)
            {
                conteudo += $"Posição {i}: {Posicoes[i]}\n";
            }
            return conteudo;
        }
    }

    //-----------------------------VERIFICAÇÃO POR ANDAR--------------------------------
    class Andar
    {
        private List<Container> Containers { get; set; }

        public Andar(List<List<string>> containers)
        {
            Containers = new List<Container>();
            foreach (var container in containers)
            {
                Containers.Add(new Container(container));
            }
        }

        public void AdicionarItem(int containerIndex, int posicaoIndex, string produto)
        {
            if (containerIndex >= 0 && containerIndex < Containers.Count)
            {
                Containers[containerIndex].AdicionarItem(posicaoIndex, produto);
            }
            else
            {
                Console.WriteLine("Índice de container inválido.");
            }
        }

        public void RemoverItem(int containerIndex, int posicaoIndex)
        {
            if (containerIndex >= 0 && containerIndex < Containers.Count)
            {
                Containers[containerIndex].RemoverItem(posicaoIndex);
            }
            else
            {
                Console.WriteLine("Índice de container inválido.");
            }
        }

        public void AdicionarItensNoContainer(int containerIndex, List<string> produtos)
        {
            if (containerIndex >= 0 && containerIndex < Containers.Count)
            {
                Containers[containerIndex].AdicionarItens(produtos);
            }
            else
            {
                Console.WriteLine("Índice de container inválido.");
            }
        }

        public void RemoverItensDoContainer(int containerIndex)
        {
            if (containerIndex >= 0 && containerIndex < Containers.Count)
            {
                Containers[containerIndex].RemoverTodosItens();
            }
            else
            {
                Console.WriteLine("Índice de container inválido.");
            }
        }

        public override string ToString()
        {
            string conteudo = "";
            for (int i = 0; i < Containers.Count; i++)
            {
                conteudo += $"Container {i}:\n{Containers[i]}";
            }
            return conteudo;
        }
    }

    class Geladeira
    {
        private List<Andar> Andares { get; set; }

        public Geladeira(List<List<List<string>>> estrutura)
        {
            Andares = new List<Andar>();
            foreach (var andar in estrutura)
            {
                Andares.Add(new Andar(andar));
            }
        }

        public void ExibirConteudo()
        {
            for (int i = 0; i < Andares.Count; i++)
            {
                Console.WriteLine($"Andar {i + 1}:\n{Andares[i]}");
            }
        }

        public void AdicionarItem(int andar, int container, int posicao, string produto)
        {
            if (andar >= 0 && andar < Andares.Count)
            {
                Andares[andar].AdicionarItem(container, posicao, produto);
            }
            else
            {
                Console.WriteLine("Índice de andar inválido.");
            }
        }

        public void RemoverItem(int andar, int container, int posicao)
        {
            if (andar >= 0 && andar < Andares.Count)
            {
                Andares[andar].RemoverItem(container, posicao);
            }
            else
            {
                Console.WriteLine("Índice de andar inválido.");
            }
        }

        public void AdicionarItensNoContainer(int andar, int container, List<string> produtos)
        {
            if (andar >= 0 && andar < Andares.Count)
            {
                Andares[andar].AdicionarItensNoContainer(container, produtos);
            }
            else
            {
                Console.WriteLine("Índice de andar inválido.");
            }
        }

        public void RemoverItensDoContainer(int andar, int container)
        {
            if (andar >= 0 && andar < Andares.Count)
            {
                Andares[andar].RemoverItensDoContainer(container);
            }
            else
            {
                Console.WriteLine("Índice de andar inválido.");
            }
        }

        //Menu de interação do usuário.
        public void Menu()
        {
            while (true)
            {
                Console.WriteLine("Escolha uma opção:");
                Console.WriteLine("1. Exibir conteúdo da geladeira");
                Console.WriteLine("2. Adicionar item em uma posição");
                Console.WriteLine("3. Remover item de uma posição");
                Console.WriteLine("4. Adicionar itens em um container");
                Console.WriteLine("5. Remover todos os itens de um container");
                Console.WriteLine("6. Sair");
                Console.Write("Opção: ");
                var opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        ExibirConteudo();
                        break;
                    case "2":
                        AdicionarItemMenu();
                        break;
                    case "3":
                        RemoverItemMenu();
                        break;
                    case "4":
                        AdicionarItensNoContainerMenu();
                        break;
                    case "5":
                        RemoverItensDoContainerMenu();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
        }

        private void AdicionarItemMenu()
        {
            Console.Write("Digite o andar (1-3): ");
            int andar = int.Parse(Console.ReadLine()) - 1;

            Console.Write("Digite o container (0-1): ");
            int container = int.Parse(Console.ReadLine());

            Console.Write("Digite a posição (0-3): ");
            int posicao = int.Parse(Console.ReadLine());

            Console.Write("Digite o nome do produto: ");
            string produto = Console.ReadLine();

            AdicionarItem(andar, container, posicao, produto);
        }

        private void RemoverItemMenu()
        {
            Console.Write("Digite o andar (1-3): ");
            int andar = int.Parse(Console.ReadLine()) - 1;

            Console.Write("Digite o container (0-1): ");
            int container = int.Parse(Console.ReadLine());

            Console.Write("Digite a posição (0-3): ");
            int posicao = int.Parse(Console.ReadLine());

            RemoverItem(andar, container, posicao);
        }

        private void AdicionarItensNoContainerMenu()
        {
            Console.Write("Digite o andar (1-3): ");
            int andar = int.Parse(Console.ReadLine()) - 1;

            Console.Write("Digite o container (0-1): ");
            int container = int.Parse(Console.ReadLine());

            var produtos = new List<string>();
            Console.WriteLine("Digite os produtos (separados por vírgula): ");
            string entrada = Console.ReadLine();
            produtos.AddRange(entrada.Split(','));

            AdicionarItensNoContainer(andar, container, produtos);
        }

        private void RemoverItensDoContainerMenu()
        {
            Console.Write("Digite o andar (1-3): ");
            int andar = int.Parse(Console.ReadLine()) - 1;
        }
    } 
}