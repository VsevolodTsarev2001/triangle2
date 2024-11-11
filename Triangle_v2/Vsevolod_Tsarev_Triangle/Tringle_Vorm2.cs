using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace Tringle
{
    public partial class Tringle_Vorm2 : Form
    {
        private double sideA;   // Külg A
        private double height;  // Kõrgus
        private Panel drawingPanel;  // Paneel kolmnurga joonistamiseks
        private DataGridView dataGridView; // Tabel kolmnurga teabe kuvamiseks
        private Triangle triangle; // Kolmnurga objekti salvestamiseks

        public Tringle_Vorm2()
        {
            SetupUI();
        }

        private void SetupUI()
        {
            this.Text = "Minu kolmnurk";
            this.Size = new Size(550, 600);
            this.BackColor = Color.LightGreen;

            Label labelA = new Label
            {
                Text = "Külg A:",
                Location = new Point(10, 20),
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.Red
            };
            Controls.Add(labelA);

            TextBox textBoxA = new TextBox
            {
                Name = "textBoxA",
                Location = new Point(150, 20),
                Size = new Size(100, 30)
            };
            Controls.Add(textBoxA);

            Label labelHeight = new Label
            {
                Text = "Kõrgus:",
                Location = new Point(10, 60),
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.Red
            };
            Controls.Add(labelHeight);

            TextBox textBoxHeight = new TextBox
            {
                Name = "textBoxHeight",
                Location = new Point(150, 60),
                Size = new Size(100, 30)
            };
            Controls.Add(textBoxHeight);

            Button calculateButton = new Button
            {
                Text = "Arvuta",
                Location = new Point(150, 100),
                Size = new Size(100, 40),
                BackColor = Color.LightYellow,
                Font = new Font("Arial", 12, FontStyle.Regular)
            };
            calculateButton.Click += CalculateButton_Click;
            Controls.Add(calculateButton);

            // Paneel kolmnurga joonistamiseks
            drawingPanel = new Panel
            {
                Size = new Size(300, 300),
                Location = new Point(200, 150),
                BackColor = Color.White
            };
            Controls.Add(drawingPanel);
            drawingPanel.Paint += DrawingPanel_Paint;

            // Tabel kolmnurga andmete kuvamiseks
            dataGridView = new DataGridView
            {
                ColumnCount = 2,
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                ColumnHeadersVisible = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells,
                Location = new Point(10, 150),
                Size = new Size(180, 250)
            };

            dataGridView.Columns[0].Name = "Väli"; // Väli
            dataGridView.Columns[1].Name = "Väärtus"; // Väärtus
            Controls.Add(dataGridView);

            // Nuut salvestamiseks XML faili
            Button saveButton = new Button
            {
                Text = "Salvesta Andmed",
                Location = new Point(150, 400),
                Size = new Size(150, 40),
                BackColor = Color.LightBlue,
                Font = new Font("Arial", 12, FontStyle.Regular)
            };
            saveButton.Click += SaveButton_Click;
            Controls.Add(saveButton);
        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            if (double.TryParse(Controls["textBoxA"].Text, out sideA) &&
                double.TryParse(Controls["textBoxHeight"].Text, out height))
            {
                // Loome uue kolmnurga ja arvutame selle omadused
                triangle = new Triangle(sideA, height);
                UpdateDataGridView();
                drawingPanel.Invalidate(); // Обновляем панель для перерисовки
            }
            else
            {
                MessageBox.Show("Palun sisestage kehtivad väärtused.");
            }
        }

        // Meetod teabe värskendamiseks tabelis
        private void UpdateDataGridView()
        {
            dataGridView.Rows.Clear();
            dataGridView.Rows.Add("Külg A", triangle.GetSetSideA);
            dataGridView.Rows.Add("Kõrgus", triangle.GetSetHeight);
            dataGridView.Rows.Add("Pindala", Math.Round(triangle.AreaWithHeight(), 2));
            dataGridView.Rows.Add("Kas eksisteerib?", triangle.ExistTriangle2 ? "Eksisteerib" : "Ei eksisteeri");
            dataGridView.Rows.Add("Spetsifikaator", triangle.TriangleTypeByBaseHeight);
        }

        // Kolmnurga joonistamine paneelil
        private void DrawingPanel_Paint(object sender, PaintEventArgs e)
        {
            if (triangle != null && triangle.ExistTriangle2)
            {
                // Skalavimo tegur, et kolmnurk ei läheks välja paneeli piiridest
                double scale = Math.Min(drawingPanel.Width / sideA, drawingPanel.Height / height) * 0.8;

                int centerX = drawingPanel.Width / 2;
                int centerY = drawingPanel.Height / 2;

                // Loome kolmnurga tipu punktid
                Point[] points = new Point[3];
                points[0] = new Point(centerX, centerY - (int)(height * scale)); // Ülemine tipp
                points[1] = new Point(centerX - (int)(sideA * scale / 2), centerY + (int)(height * scale / 2)); // Vasak tipp
                points[2] = new Point(centerX + (int)(sideA * scale / 2), centerY + (int)(height * scale / 2)); // Parem tipp

                // Joonista kolmnurk sinise värviga
                using (Pen pen = new Pen(Color.Blue, 3))
                {
                    e.Graphics.DrawPolygon(pen, points);
                }
            }
        }

        // Meetod andmete salvestamiseks XML faili
        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (triangle != null)
            {
                SaveTriangleDataToXml();
            }
            else
            {
                MessageBox.Show("Palun arvutage kolmnurga andmed enne salvestamist.");
            }
        }

        private void SaveTriangleDataToXml()
        {
            XmlDocument xmlDoc = new XmlDocument();

            // Loome juurelemendi, kui fail ei eksisteeri
            if (File.Exists("kolmnurgad.xml"))
            {
                xmlDoc.Load("kolmnurgad.xml");
            }
            else
            {
                XmlElement root = xmlDoc.CreateElement("Triangles");
                xmlDoc.AppendChild(root);
            }

            XmlElement rootElement = xmlDoc.DocumentElement;

            XmlElement triangleElement = xmlDoc.CreateElement("Triangle");

            XmlElement sideAElement = xmlDoc.CreateElement("SideA");
            sideAElement.InnerText = triangle.GetSetA.ToString();
            triangleElement.AppendChild(sideAElement);

            XmlElement heightElement = xmlDoc.CreateElement("Height");
            heightElement.InnerText = triangle.GetSetHeight.ToString();
            triangleElement.AppendChild(heightElement);

            XmlElement areaElement = xmlDoc.CreateElement("Area");
            areaElement.InnerText = triangle.AreaWithHeight().ToString();
            triangleElement.AppendChild(areaElement);

            rootElement.AppendChild(triangleElement);

            xmlDoc.Save("kolmnurgad.xml");
            MessageBox.Show("Andmed on salvestatud XML-faili.");
        }
    }
}
