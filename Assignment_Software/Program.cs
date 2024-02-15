using System.Net.Mail;
using Shapes;

internal class Program
{
    private static void Main(string[] args)
    {

        // Ans 1 Drawing shapes using Compite Design Pattern 
        IElement line1 =new Line(new Point(2,1),new Point(5,1),10);

        IElement line2=new Line(new Point(5,1),new Point(3,3),5);

        IElement line3=new Line(new Point(2,1),new Point(3,3),6);

        List <IElement> lst= new List<IElement> ();

        lst.Add(line1);

        lst.Add(line2);

        lst.Add(line3);

        IElement triangle = new Polygon(lst);

        triangle.draw();


        //Ans 2 Ability to Sort Edge weights

        triangle.getSortedWeights(new MergeSort());

        Console.WriteLine();

        // Decorate the lines

        IElement line4 =new Line(new Point(7,8),new Point(10,11),11);

        IElement line5=new Line(new Point(10,11),new Point(12,8),5);

        IElement line6=new Line(new Point(7,8),new Point(12,8),2);

        line4=new ArrowedElement(line4);
        
        line5=new OvaledElement(line5);

        line6= new OvaledElement(line6);

        IElement tr2=new Polygon([line4,line5,line6]);

        tr2.draw();


        // Ans 4 and 5 Creating  an abstract factory of points anf Lines. Each of the abstract factory is a Singleton. Similaly for Polygons it is possible.
        
        IElementFactory ef=RhombP_DashedL_Factory.getFactoryInstance();
        
        IElementFactory ef1=CircP_BoldL_Factory.getFactoryInstance();

        IElement point8=ef.getPoint(6,9);
        IElement point9=ef1.getPoint(5,5);
        IElement point10=ef1.getPoint(2,1);

        IElement line7=ef.getLine(point8,point9,14);
       
        //point8.draw();
        
        IElement line8=ef1.getLine(point9,point10,34);

        IElement line9=ef.getLine(point10,point8,3);

        IElement Tr5=new Polygon([line7,line8,line8]);

        Tr5.draw(); 
    
    
        //Ans 5 
    
    }

}