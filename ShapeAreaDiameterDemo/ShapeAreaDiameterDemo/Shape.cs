using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ShapeAreaDiameterDemo
{
    public abstract class Shape
    {
        // Abstract properties for Area and Diameter
        // These properties must be implemented by derived classes
        // to provide specific calculations for each shape.

        public const double PI = Math.PI; // Constant for PI
        protected double _x, _y;

        protected Shape()
        {
        }

        public Shape(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public virtual double CalculateArea()
        {
            return _x * _y; // Default implementation, can be overridden
        }
        public virtual double CalculateDiameter()
        {
            return (_x * 2 + _y * 2); // Default implementation, can be overridden
        }
    }

    public class Circle : Shape
    {
        // Circle specific properties
        private double _radius;

        public double Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }
        public Circle(double radius): base(radius, 0) // Assuming _x and _y are both radius for Circle
        {
            Radius = radius;
        }
        
        public override double CalculateArea()
        {
            // Area = π * r^2
            return PI * Radius * Radius; // Assuming _x is the radius
        }

        public override double CalculateDiameter()
        {
            // Diameter = 2 * r
            return 2 * Radius * PI; // Assuming _x is the radius
        }

    }


    public class Rectangle : Shape 
    {
        private double _width;
        private double _height;

        // Rectangle specific properties
        public double Width
        {
            get { return _width; }
            set { _width = value > 0 ? value : throw new ArgumentException("Width must be positive"); }
        }

        public double Height
        {
            get { return _height; }
            set { _height = value > 0 ? value : throw new ArgumentException("Height must be positive"); }
        }

        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public override double CalculateArea()
        {
            return Width * Height; // Area = width * height
        }

        public override double CalculateDiameter()
        {
            return Width * 2 + Height * 2; // Diameter = 2 * (width + height)
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create a Circle and calculate its area and diameter
            Circle circle = new Circle(5);
            Console.WriteLine($"Circle Area: {circle.CalculateArea()}");
            Console.WriteLine($"Circle Diameter: {circle.CalculateDiameter()}");
            // Create a Rectangle and calculate its area and diameter
            Rectangle rectangle = new Rectangle(4, 6);
            Console.WriteLine($"Rectangle Area: {rectangle.CalculateArea()}");
            Console.WriteLine($"Rectangle Diameter: {rectangle.CalculateDiameter()}");
        }
    }
}

