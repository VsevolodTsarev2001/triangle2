using System;

namespace Tringle
{
    public class Triangle
    {
        // Külg A, B, C
        private double A;
        private double B;
        private double C;

        // Kolmnurga külg ja kõrgus (baasi ja kõrguse kolmnurk)
        private double sideA;
        private double height;

        public double S; // Pindala
        public double H; // Kõrgus (baasi kolmnurga jaoks)

        // Konstruktor kolmnurga jaoks kolme külje järgi
        public Triangle(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
        }

        // Konstruktor baasi ja kõrguse jaoks
        public Triangle(double sideA, double height)
        {
            this.sideA = sideA;
            this.height = height;
        }

        // Tühja konstruktor
        public Triangle() { }

        // Kontroll kolmnurga olemasolu kohta
        public bool ExistTriangle
        {
            get
            {
                if (A + B > C && A + C > B && B + C > A)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool ExistTriangle2
        {
            get
            {
                return (A > 0 && H > 0);
                
            }
        }
        // Omadused külgede jaoks
        public double GetSetA
        {
            get { return A; }
            set { A = value; }
        }

        public double GetSetB
        {
            get { return B; }
            set { B = value; }
        }

        public double GetSetC
        {
            get { return C; }
            set { C = value; }
        }

        public double GetSetSideA
        {
            get { return sideA; }
            set { sideA = value; }
        }

        public double GetSetHeight
        {
            get { return height; }
            set { height = value; }
        }

        // Kolmnurga ümbermõõdu arvutamine kolme külje järgi
        public double Perimeter()
        {
            return A + B + C;
        }

        // Pool ümbermõõdu arvutamine Heroni valemi jaoks
        public double Perimeter1_2()
        {
            return (A + B + C) / 2;
        }

        // Pindala arvutamine kolme külje järgi (Heroni valem)
        public double Area()
        {
            double s = 0;
            if (ExistTriangle)
            {
                double p = Perimeter1_2();
                s = Math.Sqrt(p * (p - A) * (p - B) * (p - C));
            }
            return s;
        }

        // Pindala arvutamine baasi ja kõrguse järgi
        public double AreaWithHeight()
        {
            return 0.5 * sideA * height;
        }

        // Kolmnurga tüübi määramine kolme külje järgi
        public string TriangleType
        {
            get
            {
                if (ExistTriangle)
                {
                    if (A == B && B == C && A == C)
                    {
                        return "Võrdkülgne";
                    }
                    else if (A == B || A == C || B == C)
                    {
                        return "Võrdhaarne";
                    }
                    else
                    {
                        return "Erinev külg";
                    }
                }
                else
                {
                    return "Ei eksisteeri";
                }
            }
        }

        // Kolmnurga tüübi määramine baasi ja kõrguse järgi
        public string TriangleTypeByBaseHeight
        {
            get
            {
                if (sideA == height) return "Võrdhaarne (baasi ja kõrguse järgi)";
                else if (sideA != height) return "Erinev külg (baasi ja kõrguse järgi)";
                else return "Tundmatu";
            }
        }

        // Külgede väljastamine
        public string OutputA()
        {
            return A.ToString();
        }

        public string OutputB()
        {
            return B.ToString();
        }

        public string OutputC()
        {
            return C.ToString();
        }

        // Pindala väljastamine
        public string OutputArea()
        {
            return Area().ToString("F2");
        }

        // Pindala baasi ja kõrguse järgi
        public string OutputAreaWithHeight()
        {
            return AreaWithHeight().ToString("F2");
        }
    }
}
