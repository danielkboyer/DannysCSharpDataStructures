using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannysCSharpDataStructures
{
    /// <summary>
    /// min heap that includes the decrease key operation that is needed for dijkstras algo
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MinHeap<T> where T: IComparable<T>
    {
        private List<T> array = new List<T>();

        private Dictionary<T, int> map = new Dictionary<T, int>();

        /// <summary>
        /// swaps the two elements and updates the maps with the new indexes
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        private void Swap(int index1, int index2)
        {
            T tmp = array[index1];
            array[index1] = array[index2];
            array[index2] = tmp;

            //update dictionary values
            //values MUST always be present in dictionary
            map[tmp] = index2;
            map[array[index1]] = index1;
        }

        /// <summary>
        /// O(logn)
        /// </summary>
        /// <param name="element"></param>
        public void Add(T element)
        {
            array.Add(element);
            int n = array.Count - 1;
            //add the new element to the map
            map.Add(element, n);
            //we need to bubble this element as far as we can to the top
            while (n > 0 && array[n].CompareTo(array[n/2]) == -1) //as long as the element above it in the tree is greater than this element we just added
            {
                //perform a swap of the two elements
                Swap(n, n / 2);
                n = n / 2;
            }
        }

        /// <summary>
        /// O(1)
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            return array[0];
        }

        public int Count
        {
            get
            {
                return array.Count;
            }
        }

        /// <summary>
        /// O(logn) assumes that the new key is less than the old key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="newKey"></param>
        public void DecreaseKey(T key, T newKey)
        {
            if (!map.ContainsKey(key))
                throw new Exception($"No key to decrease for key {key}");
            int index = map[key];
            //remove the key from the map
            map.Remove(key);

            array[index] = newKey;

            map.Add(newKey, index);
            int c = index;
            while (c > 0 && array[c].CompareTo(array[c / 2]) == -1)
            {
                Swap(c, c / 2);
                c = c / 2;
            }


        }
        /// <summary>
        /// O(logn)
        /// </summary>
        /// <returns></returns>
        public T RemoveMin()
        {
            T retrieved = array[0];
            //remove the retrieved element from the map
            map.Remove(retrieved);

            //place the last element in the tree at the top and bubble down
            array[0] = array[array.Count - 1];
            array.RemoveAt(array.Count - 1);

            int n = 0;

            while(n < array.Count)
            {
                int min = n;
                //the node to the bottom right of the current node
                int botLeft = 2 * n + 1;
                //the node to the bottom left of the current node
                int botRight = 2 * n + 2;
                //if the bot left is less than the current min node then we assign this as the new min node 
                if(botLeft < array.Count && array[botLeft].CompareTo(array[min]) == -1)
                {
                    min = botLeft;
                }
                //if the bot right is less than the current min node then we assign this as the new min node
                if(botRight < array.Count && array[botRight].CompareTo(array[min]) == -1)
                {
                    min = botRight;
                }
                //if the node at top is the smallest than we are finished
                if (min == n)
                    break;

                //perform a swap with the min node and the node above it
                T tmp = array[n];
                array[n] = array[min];
                array[min] = tmp;
                n = min;
            }

            return retrieved;
        }
    }
}
