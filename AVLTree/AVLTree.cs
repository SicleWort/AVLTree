using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVLTree
{
    public class AVLTree<T> : IEnumerable<T> where T : IComparable
    {
        public int Iterations;
        public AVLTreeNode<T> Head
        {
            get;
            internal set;
        }
        public int Count
        {
            get;
            private set;
        }

        public void Add(T value)
        {
            // Вариант 1:  Дерево пустое - создание корня дерева      
            if (Head == null)
            {
                Iterations++;
                Head = new AVLTreeNode<T>(value, null, this);
            }
            // Вариант 2: Дерево не пустое - найти место для добавление нового узла.
            else
                AddTo(Head, value);

            Count++;
        }
        private void AddTo(AVLTreeNode<T> node, T value)
        {
            // Вариант 1: Добавление нового узла в дерево. Значение добавлемого узла меньше чем значение текущего узла.      
            Iterations++;
            if (value.CompareTo(node.Value) < 0)
            {
                //Создание левого узла, если его нет.
                if (node.Left == null)
                {
                    node.Left = new AVLTreeNode<T>(value, node, this);
                }
                else
                {
                    // Переходим к следующему левому узлу
                    AddTo(node.Left, value);
                }
            }
            // Вариант 2: Добавлемое значение больше или равно текущему значению.
            else
            {
                //Создание правого узла, если его нет.         
                if (node.Right == null)
                {
                    node.Right = new AVLTreeNode<T>(value, node, this);
                }
                else
                {
                    // Переход к следующему правому узлу.             
                    AddTo(node.Right, value);
                }
            }
        }
        public bool Contains(T value)
        {
            return Find(value) != null;
        }
        /// <summary> 
        /// Находит и возвращает первый узел который содержит искомое значение.
        /// Если значение не найдено, возвращает null. 
        /// Так же возвращает родительский узел.
        /// </summary> /// 
        /// <param name="value">Значение поиска</param> 
        /// <param name="parent">Родительский элемент для найденного значения/// </param> 
        /// <returns> Найденный узел (или ноль) /// </returns> 

        public AVLTreeNode<T> Find(T value)
        {
            AVLTreeNode<T> current = Head; // помещаем текущий элемент в корень дерева
         
            while (current != null)   // Пока текщий узел не пустой 
            {
                Iterations++;
                int result = current.CompareTo(value); // сравнение значения текущего элемента с искомым значением
                if (result > 0)
                {
                    // Если значение меньше текущего - переход влево 
                    current = current.Left;
                }
                else if (result < 0)
                {
                    // Если значение больше текщего - переход вправо             
                    current = current.Right;
                }
                else
                {
                    // Элемент найден      
                    break;
                }
            }
            return current;
        }
        public bool Remove(T value)
        {
            Iterations++;
            AVLTreeNode<T> current;
            current = Find(value); // находим узел с удаляемым значением

            if (current == null) // узел не найден
                return false;

            AVLTreeNode<T> treeToBalance = current.Parent; // баланс дерева относительно узла родителя
            Count--; // уменьшение количества узлов

            // Вариант 1: Если удаляемый узел не имеет правого потомка
            if (current.Right == null) // если нет правого потомка
            {
                if (current.Parent == null) // удаляемый узел является корнем
                {
                    Head = current.Left;  // на место корня перемещаем левого потомка

                    if (Head != null)
                        Head.Parent = null;  // убираем ссылку на родителя  
                }
                else // удаляемый узел не является корнем
                {
                    int result = current.Parent.CompareTo(current.Value);

                    if (result > 0)  
                        // Если значение родительского узла больше значения удаляемого,
                        // сделать левого потомка удаляемого узла, левым потомком родителя.  
                        current.Parent.Left = current.Left;

                    else if (result < 0)
                        // Если значение родительского узла меньше чем удаляемого,                 
                        // сделать левого потомка удаляемого узла - правым потомком родительского узла.        
                        current.Parent.Right = current.Left;
                }
            }
            // Вариант 2: Если правый потомок удаляемого узла не имеет левого потомка,
            // тогда правый потомок удаляемого узла становится потомком родительского узла.      
            else if (current.Right.Left == null) // если у правого потомка нет левого потомка
            {
                current.Right.Left = current.Left;

                if (current.Parent == null) // текущий элемент является корнем
                {
                    Head = current.Right;

                    if (Head != null)
                        Head.Parent = null;
                }
                else
                {
                    int result = current.Parent.CompareTo(current.Value);
        
                    if (result > 0)
                        // Если значение узла родителя больше чем значение удаляемого узла,                 
                        // сделать правого потомка удаляемого узла, левым потомком его родителя.      
                        current.Parent.Left = current.Right;

                    else if (result < 0)
                        // Если значение родительского узла меньше значения удаляемого,                 
                        // сделать правого потомка удаляемого узла - правым потомком родителя.                 
                        current.Parent.Right = current.Right;
                }
            }

            // Вариант 3: Если правый потомок удаляемого узла имеет левого потомка,      
            // заменить удаляемый узел крайним левым потомком правого потомка.     
            else
            {
                // Нахождение крайнего левого узла для правого потомка удаляемого узла.       
                AVLTreeNode<T> leftmost = current.Right.Left;

                while (leftmost.Left != null)
                {
                    leftmost = leftmost.Left;
                }
                // Родительское правое поддерево становится родительским левым поддеревом.         
                leftmost.Parent.Left = leftmost.Right;
                // Присвоить крайнему левому узлу, ссылки на правого и левого потомка удаляемого узла.       
                leftmost.Left = current.Left;
                leftmost.Right = current.Right;

                if (current.Parent == null)
                {
                    Head = leftmost;
                    if (Head != null)
                        Head.Parent = null;
                }
                else
                {
                    int result = current.Parent.CompareTo(current.Value);

                    // Если значение родительского узла больше значения удаляемого,                 
                    // сделать крайнего левого потомка левым потомком родителя удаляемого узла.                 
                    if (result > 0)
                        current.Parent.Left = leftmost;

                    // Если значение родительского узла, меньше чем значение удаляемого,                 
                    // сделать крайнего левого потомка, правым потомком родителя удаляемого узла.                 
                    else if (result < 0)
                        current.Parent.Right = leftmost;
                }
            }

            if (treeToBalance != null)
            {
                Iterations++;
                treeToBalance.Balance();
            }


            else
            {
                if (Head != null)
                {
                    Iterations++;
                    Head.Balance();
                }
            }

            return true;

        }
        public void Clear()
        {
            Head = null; 
            Count = 0;
        }

        public IEnumerator<T> InOrderTraversal()
        {
            if (Head != null)
            {
                Stack<AVLTreeNode<T>> stack = new Stack<AVLTreeNode<T>>();
                AVLTreeNode<T> current = Head;

                bool goLeftNext = true;

                stack.Push(current);

                while (stack.Count > 0)
                {
                    if (goLeftNext)
                    {
                        while (current.Left != null)
                        {
                            stack.Push(current);
                            current = current.Left;
                        }
                    }

                    yield return current.Value;

                    if (current.Right != null)
                    {
                        current = current.Right;
                        goLeftNext = true;
                    }
                    else
                    {
                        current = stack.Pop();
                        goLeftNext = false;
                    }
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return InOrderTraversal();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}