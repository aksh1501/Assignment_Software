using System.Reflection;
using System;
using System.Security.Principal;

namespace Shapes
{
    interface IElement
    {
        void draw();

        int getWeight();

        void getSortedWeights(sortAlgorithm s);
    }

    class Point: IElement
    {
        private int x,y;
        private int weight=0;

        public Point(int x,int y)
        {
            this.x = x;
            this.y = y;
        }

        virtual public void draw()
        {
            Console.Write(" Point (" + x + " , " + y + ") ");
        }


        public void getSortedWeights(sortAlgorithm s)
        {
            Console.WriteLine();
        }

        public int getWeight()
        {
            return 0;
        }
    }

    class Line : IElement
    {

        IElement end1;
        IElement end2;

        private int weight;

        public Line( IElement s, IElement e, int w)
        {
            end1=s;
            end2=e;
            weight=w;
        }

        virtual public void draw()
        {
            Console.Write("A normal line joining :  ");
            end1.draw();
            Console.Write(", ");
            end2.draw();
            Console.WriteLine(" with weight " + weight);
        }

        public int getWeight()
        {
            return weight;
        }

        public void getSortedWeights(sortAlgorithm s)
        {
            Console.WriteLine("Weight is" + weight);
        }
    }

    class Polygon : IElement
    {

        List <IElement> components;

        public Polygon(List <IElement> components)
        {
            this.components=components;
        }

        public void draw()
        {
            Console.WriteLine("Drawing a shape with :  ");

            foreach(IElement e in components)
            {
                e.draw();
            }

            Console.WriteLine();
        }

        public int getWeight()
        {
            int totalWeight=0;

            foreach(Line e in components)
            {
                totalWeight+=e.getWeight();
            }

            return totalWeight;
        }

        public void getSortedWeights(sortAlgorithm algo)
        {
            Sorter s=new Sorter(algo);

            List <int> weightList=new List<int> ();

            foreach(Line e in components)
            {
                weightList.Add(e.getWeight());
            }

            s.Sort(weightList);

            Console.Write("List of Sorted Weights : ");

            foreach(int x in weightList)
            {
                Console.Write(" " + x + " ,");
            }

            Console.WriteLine();
        }

    }

    class Sorter
    {
        private sortAlgorithm sortAlgo;

        public Sorter(sortAlgorithm algorithm)
        {
            sortAlgo=algorithm;
        }

        public void Sort(List <int> l)
        {
            sortAlgo.sort(l);
        }  
    }

    interface sortAlgorithm
    {
        void sort(List <int> l);
    }



    class MergeSort : sortAlgorithm
    {
        public void sort(List<int> list)
        {
            if (list.Count <= 1)
                return;

            int middle = list.Count / 2;
            List<int> left = new List<int>();
            List<int> right = new List<int>();

            for (int i = 0; i < middle; i++)
            {
                left.Add(list[i]);
            }
            for (int i = middle; i < list.Count; i++)
            {
                right.Add(list[i]);
            }

            sort(left);
            sort(right);
            merge(left, right, list);
        }

        private void merge(List<int> left, List<int> right, List<int> merged)
        {
            int leftIndex = 0, rightIndex = 0, mergedIndex = 0;

            while (leftIndex < left.Count && rightIndex < right.Count)
            {
                if (left[leftIndex] <= right[rightIndex])
                {
                    merged[mergedIndex] = left[leftIndex];
                    leftIndex++;
                }
                else
                {
                    merged[mergedIndex] = right[rightIndex];
                    rightIndex++;
                }
                mergedIndex++;
            }

            while (leftIndex < left.Count)
            {
                merged[mergedIndex] = left[leftIndex];
                leftIndex++;
                mergedIndex++;
            }

            while (rightIndex < right.Count)
            {
                merged[mergedIndex] = right[rightIndex];
                rightIndex++;
                mergedIndex++;
            }
        }
    }


    class QuickSort : sortAlgorithm
    {
        public void sort(List <int> l)
        {
            QuickSortRecursive(l, 0, l.Count - 1);
        }

        private void QuickSortRecursive(List<int> list, int left, int right)
        {
            if (left < right)
            {
                int partitionIndex = Partition(list, left, right);

                QuickSortRecursive(list, left, partitionIndex - 1);
                QuickSortRecursive(list, partitionIndex + 1, right);
            }
        }      

        private int Partition(List<int> list, int left, int right)
        {
            int pivot = list[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (list[j] <= pivot)
                {
                    i++;

                    int temp = list[i];
                    list[i] = list[j];
                    list[j] = temp;
                }
            }

            int temp1 = list[i + 1];
            list[i + 1] = list[right];
            list[right] = temp1;

            return i + 1;
        }
    }


    abstract class ElementWrapper : IElement
    {
        IElement WrappedElement;

        public ElementWrapper(IElement e) 
        {
            WrappedElement = e;
        }

        virtual public void draw()
        {
            WrappedElement.draw();
        }

        public void getSortedWeights(sortAlgorithm s)
        {
            WrappedElement.getSortedWeights(s);
        }

        public int getWeight()
        {
            return WrappedElement.getWeight();
        }
    }

    class ArrowedElement : ElementWrapper
    {
        public ArrowedElement(IElement e) : base(e)
        {
        }

        override public void draw()
        {
            base.draw();
            AddArrows();
        }

        void AddArrows()
        {
            Console.WriteLine("Added Arrows to the Line.");
            Console.WriteLine();
        }
    }


   class OvaledElement : ElementWrapper
    {
        public OvaledElement(IElement e) : base(e)
        {
        }

        override public void draw()
        {
            base.draw();
            AddOvals();
        }

        void AddOvals()
        {
            Console.WriteLine("Added Ovals to the line.");
            Console.WriteLine();
        }
    }

    class DashedLine : Line
    {
        public DashedLine(IElement s, IElement e, int w) : base(s, e, w)
        {
        }

        override public void draw()
        {
            base.draw();
            Console.Write(" [This is a dashed Line.] ");
            Console.WriteLine();
            Console.WriteLine();
        }
    }


    class BoldLine : Line
    {
        public BoldLine(IElement s, IElement e, int w) : base(s, e, w)
        {
        }

        override public void draw()
        {
            base.draw();
            Console.Write(" [This is a super bold line.] ");
            Console.WriteLine();
            Console.WriteLine();
        }
    }


    class RhombicPoint : Point
    {
        public RhombicPoint(int x, int y) : base(x, y)
        {
        }

        override public void draw()
        {
            base.draw();
            Console.Write(" [This is a Rhombic Point.] ");
        }
    }


     class CircledPoint : Point
    {
        public CircledPoint(int x, int y) : base(x, y)
        {
        }

        override public void draw()
        {
            base.draw();
            Console.Write(" [This is a Circled Point.] ");
        }
    }


    interface IElementFactory
    {
        IElement getPoint(int x,int y);
        IElement getLine(IElement s, IElement e, int w);
    }



    class CircP_BoldL_Factory : IElementFactory
    {
        static CircP_BoldL_Factory instance = null;

        private CircP_BoldL_Factory()
        {

        }

        static public  CircP_BoldL_Factory getFactoryInstance()
        {
            if(instance==null)
            {
                instance=new CircP_BoldL_Factory();
            }

            return instance;
        }
        public IElement getLine(IElement s, IElement e, int w)
        {
            return new BoldLine(s,e,w);
        }

        public IElement getPoint(int x,int y)
        {
           return new CircledPoint(x,y);
        }
    }

    class RhombP_DashedL_Factory : IElementFactory
    {
        static RhombP_DashedL_Factory instance=null;

        private RhombP_DashedL_Factory(){}

        static public RhombP_DashedL_Factory getFactoryInstance()
        {
            if(instance==null)
            {
                instance=new RhombP_DashedL_Factory();
            }

            return instance;
        }

        public IElement getLine(IElement s, IElement e, int w)
        {
            return new DashedLine(s,e,w);
        }

        public IElement getPoint(int x, int y)
        {
            return new RhombicPoint(x,y);
        }
    }


}