using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using System.Numerics;
using System.Diagnostics;

public partial class main : System.Web.UI.Page
{
    private double Am = 0;
    private double fzas = 0;
    private double[,] Results;
    private int size = 0;
    private double C;
    private double R1;
    private double R2;
    private double L;

    private double Zc;
    private double Zl;

    private double freqStart;
    private double freqEnd;

    private double[,] Results2;

    private double min3, max3;
    private String error = "";
    private String dateAndTime;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            panWaveform.Visible = false;
            panWaveform2.Visible = false;
            panWaveform3.Visible = false;
        }
        else
        {
            panWaveform.Visible = true;
            panWaveform2.Visible = true;
            panWaveform3.Visible = true;
        }

        labMessage.Text = "Poproszę o podanie wartości poszczególnych elementów i kliknięcie w odpowiedni przycisk, by wygenerować pożądaną charakterystykę.";
    }

    private void Calculate()

    {
        getCurrentTimeandDate();

        labMessage.Text = "";
        if (!Double.TryParse(txtMagnitude.Text, out Am))
        {
            labMessage.Text = dateAndTime + "Błędna wartość amplitudy.";
            error = "amplitudy.";
            return;
        }

        if (Am <= 0)
        {
            clearChartSpace();
            labMessage.Text = dateAndTime + "Błędna wartość amplitudy.";
            if (Am == 0) error = "amplitudy. Wartość nie może być równa zeru.";
            if (Am < 0) error = "amplitudy. Wartość nie może być ujemna.";
        }

        if (!Double.TryParse(txtFrequency.Text, out fzas))
        {
            labMessage.Text = dateAndTime + "Błędna wartość czestotliwosci.";
            error = "częstotliwości.";
            return;
        }

        if (fzas <= 0)
        {
            clearChartSpace();
            labMessage.Text = dateAndTime + "Błędna wartość częstotliwości.";
            if (fzas == 0) error = "częstotliwości. Wartość nie może być równa zeru.";
            if (fzas < 0) error = "częstotliwości. Wartość nie może być ujemna.";
        }

        if (!Double.TryParse(txtRes1.Text, out R1))
        {
            labMessage.Text = dateAndTime + "Błędna wartość rezystancji R1.";
            error = "rezystancji R1.";
            return;
        }

        if (R1 <= 0)
        {
            clearChartSpace();
            labMessage.Text = dateAndTime + "Błędna wartość rezystancji R1.";
            if (R1 == 0) error = "rezystancji R1. Wartość nie może być równa zeru.";
            if (R1 < 0) error = "rezystancji R1. Wartość nie może być ujemna.";
        }

        if (!Double.TryParse(txtRes2.Text, out R2))
        {
            labMessage.Text = dateAndTime + "Błędna wartość rezystancji R2.";
            error = "rezystancji R2.";
            return;
        }

        if (R2 <= 0)
        {
            clearChartSpace();
            labMessage.Text = dateAndTime + "Błędna wartość rezystancji R2.";
            if (R2 == 0) error = "rezystancji R2. Wartość nie może być równa zeru.";
            if (R2 < 0) error = "rezystancji R2. Wartość nie może być ujemna.";
        }

        if (!Double.TryParse(txtCap.Text, out C))
        {
            labMessage.Text = dateAndTime + "Błędna wartość pojemności.";
            error = "pojemności C.";
            return;
        }

        if (C <= 0)
        {
            clearChartSpace();
            labMessage.Text = dateAndTime + "Błędna wartość pojemności C.";
            if (C == 0) error = "pojemności C. Wartość nie może być równa zeru.";
            if (C < 0) error = "pojemności C. Wartość nie może być ujemna.";
        }

        if (!Double.TryParse(txtInd.Text, out L))
        {
            labMessage.Text = dateAndTime + "Błędna wartość indukcyjności.";
            error = "indukcyjności L.";
            return;
        }

        if (L <= 0)
        {
            clearChartSpace();
            labMessage.Text = dateAndTime + "Błędna wartość indukcyjności L.";
            if (L == 0) error = "indukcyjności L. Wartość nie może być równa zeru.";
            if (L < 0) error = "indukcyjności L. Wartość nie może być ujemna.";
        }

        if (!Double.TryParse(txtFreqStart.Text, out freqStart))
        {
            labMessage.Text = dateAndTime + "Błędna wartość częstotliwości początkowej.";
            error = "częstotliwości początkowej.";
            return;
        }

        if (freqStart <= 0)
        {
            clearChartSpace();
            labMessage.Text = dateAndTime + "Błędna wartość częstotliwości początkowej.";
            if (freqStart == 0) error = "częstotliwości początkowej. Wartość nie może być równa zeru.";
            if (freqStart < 0) error = "częstotliwości początkowej. Wartość nie może być ujemna.";
        }

        if (!Double.TryParse(txtFreqEnd.Text, out freqEnd))
        {
            labMessage.Text = dateAndTime + "Błędna wartość częstotliwości końcowej.";
            error = "częstotliwości końcowej.";
            return;
        }

        if (freqEnd <= 0)
        {
            clearChartSpace();
            labMessage.Text = dateAndTime + "Błędna wartość częstotliwości końcowej.";
            if (freqEnd == 0) error = "częstotliwości końcowej. Wartość nie może być równa zeru.";
            if (freqEnd < 0) error = "częstotliwości końcowej. Wartość nie może być ujemna.";
        }

        if (!Int32.TryParse(txtSamples.Text, out size))
        {
            labMessage.Text = dateAndTime + "Błędna wartość liczby próbek.";
            error = "próbek.";
            return;
        }

        if (size < 1000)
        {
            clearChartSpace();
            labMessage.Text = dateAndTime + "Błędna wartość liczby próbek.";
            error = "próbek. Nie wystaczająca ilość próbek. Zalecana wartość to 140 000.";
            if (size < 0) {
            error = "próbek. Wartość nie może być ujemna, musi być większa od 999.";}
            if (size == 0) { error = "próbek. Wartość nie może być równa 0, musi być większa od 999.";}
            if (size>0 && size < 1000) { error = "próbek. Wartość nie może być mniejsza niż 1000.";}
            size = 1;
        }

        if (size > 170000)
        {
            error = "próbek. Wartość nie może być większa niż 170 000.";
            labMessage.Text = dateAndTime + "Błędna wartość liczby próbek. Wartość zbyt duża, przekraczająca 170 000 próbek.";
        }

        if (dropDownList1.SelectedValue.ToString() == "1") Am = Am * 1;
        else if (dropDownList1.SelectedValue.ToString() == "2") Am = Am / 1000;
        else if (dropDownList1.SelectedValue.ToString() == "3") Am = Am * 1000;

        if (dropDownList2.SelectedValue.ToString() == "1") fzas = fzas * 1;
        else if (dropDownList2.SelectedValue.ToString() == "2") fzas = fzas * 1000;
        else if (dropDownList2.SelectedValue.ToString() == "3") fzas = fzas * 1000000;

        if (dropDownList3.SelectedValue.ToString() == "1") R1 = R1 * 1;
        else if (dropDownList3.SelectedValue.ToString() == "2") R1 = R1 * 1000;
        else if (dropDownList3.SelectedValue.ToString() == "3") R1 = R1 * 1000000;

        if (dropDownList4.SelectedValue.ToString() == "1") R2 = R2 * 1;
        else if (dropDownList4.SelectedValue.ToString() == "2") R2 = R2 * 1000;
        else if (dropDownList4.SelectedValue.ToString() == "3") R2 = R2 * 1000000;

        if (dropDownList5.SelectedValue.ToString() == "1") C = C * 1;
        else if (dropDownList5.SelectedValue.ToString() == "2") C = C / 1000;
        else if (dropDownList5.SelectedValue.ToString() == "3") C = C / 1000000;
        else if (dropDownList5.SelectedValue.ToString() == "4") C = C / 1000000000;

        if (dropDownList6.SelectedValue.ToString() == "1") L = L * 1;
        else if (dropDownList6.SelectedValue.ToString() == "2") L = L / 1000;
        else if (dropDownList6.SelectedValue.ToString() == "3") L = L / 1000000;

        if (dropDownList7.SelectedValue.ToString() == "1") freqStart = freqStart * 1;
        else if (dropDownList7.SelectedValue.ToString() == "2") freqStart = freqStart * 1000;
        else if (dropDownList7.SelectedValue.ToString() == "3") freqStart = freqStart * 1000000;

        if (dropDownList8.SelectedValue.ToString() == "1") freqEnd = freqEnd * 1;
        else if (dropDownList8.SelectedValue.ToString() == "2") freqEnd = freqEnd * 1000;
        else if (dropDownList8.SelectedValue.ToString() == "3") freqEnd = freqEnd * 1000000;

        double time = 0;

        double dFreq;

        dFreq = ((freqEnd - freqStart) / (size - 1));
        double frequency = freqStart;

        Results = new double[size, 2];
        Results2 = new double[size, 2];
        double U;

        for (int i = 0; i < size; i++)
        {
            if (i == 0) frequency = freqStart;
            else frequency = frequency + dFreq;

            time = (1 / fzas) / 4;

            Zc = -1 / (2 * Math.PI * frequency * C);
            Zl = (2 * Math.PI * frequency * L);

            Complex cZc = new Complex(0, Zc);
            Complex cL = new Complex(0, Zl);
            Complex cR1 = new Complex(R1, 0);
            Complex cR2 = new Complex(R2, 0);

            Complex Z1 = Complex.Add(cZc, cR2);
            Complex Z2 = Complex.Divide((Complex.Multiply(cR1, Z1)), (Complex.Add(cR1, Z1)));
            Complex Z3 = Complex.Add(Z2, cL);
            Complex imp = Complex.Multiply(Complex.Divide(Z2, Complex.Add(Z2, cL)), Complex.Divide(cZc, Complex.Add(cZc, cR2)));

            U = Am * Math.Sin(2 * Math.PI * fzas * time);
            Complex current = Complex.Divide(U, Z3);

            //  Debug.WriteLine(frequency.ToString());

            Results[i, 0] = frequency;

            //time = i * dt;

            // Results[i, 1] = time;

            Results[i, 1] = 20 * Math.Log10(imp.Magnitude);
            Results2[i, 0] = imp.Phase * 180 / Math.PI;
            Results2[i, 1] = current.Magnitude * 1000;
        }
        Draw();
        Draw2();
        Draw3();
    }

    private void Draw()
    {
        //deklaracja klasy kolekcji
        DataTable table;
        //obiekt widoku reprezentujacy klase kolekcji
        DataView dView;

        table = new DataTable(); // inicjalizacja obiektu table konstruktorem domyslnym
        DataColumn column; //deklaracja obiektu kolumny
        DataRow row; //deklaracja obiektu wiersza

        column = new DataColumn(); //inicjalizacja obiektu kolumny
        column.DataType = System.Type.GetType("System.Double"); //zdefiniowanie typu danych w kolumnie
        column.ColumnName = "Czestotliwosc"; //zdefiniowanie nazwy kolumny
        table.Columns.Add(column); //dodanie kolumny do klasy kolekcji

        column = new DataColumn(); //inicjalizacja obiektu kolumny
        column.DataType = System.Type.GetType("System.Double"); //zdefiniowanie typu danych w kolumnie
        column.ColumnName = "Stosunek Napiec"; //zdefiniowanie nazwy kolumny
        table.Columns.Add(column); //dodanie kolumny do klasy kolekcji

        for (int i = 0; i < size; i++)
        {
            row = table.NewRow(); //inicjalizazcja wiersza bez jawnego uzycia konstruktora
            row["Czestotliwosc"] = Results[i, 0]; //pierwsza celka  - kolumna Time
            row["Stosunek Napiec"] = Results[i, 1];  //druga celka  - kolumna Voltage
            table.Rows.Add(row); //dodanie wierszu do obiektuy table
        }

        dView = new DataView(table);
        Chart1.Series.Clear(); //wyczyszczenie wszystkich serii

        Chart1.DataBindTable(dView, "Czestotliwosc"); // skojarzenie obiektu widoku z kontrolka Chart pierwszy atgument to obiekt klasy widoku
        //drugi argument to zdefiniowanie kolumny dla osi x

        Chart1.Series["Stosunek Napiec"].ChartType = SeriesChartType.Line;
        // Chart1.Series["Current"].ChartType = SeriesChartType.Line;
        //  Chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{#0.000}"; //ustawienie formatu dla osi X

        Chart1.Height = 700;
        Chart1.Width = 1400;
        Chart1.BackColor = System.Drawing.ColorTranslator.FromHtml("#9E9E9E");

        Chart1.ChartAreas[0].BackColor = System.Drawing.Color.Gainsboro;

        Chart1.ChartAreas[0].AxisX.LineWidth = 2;

        Chart1.ChartAreas[0].AxisX.Title = "Częstotliwość [Hz]";
        Chart1.ChartAreas[0].AxisY.Title = "20*log10(U2/U1) [-]";

        Chart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Italic);
        Chart1.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Italic);

        Chart1.Titles.Clear();
        Chart1.Titles.Add("Widmo amplitudowe U2/U1");

        Chart1.Series["Stosunek Napiec"].BorderWidth = 2;

        Chart1.Series["Stosunek Napiec"].Color = System.Drawing.Color.Blue;

        Chart1.Titles[0].Font = new System.Drawing.Font("Arial", 26F, System.Drawing.FontStyle.Regular);

        Chart1.Titles[0].ForeColor = System.Drawing.Color.Black;

        Chart1.Legends[0].DockedToChartArea = Chart1.ChartAreas[0].Name;
        //Chart1.Legends[0].Docking = System.Web.UI.DataVisualization.Charting.Docking.Right;

        // Chart1.ChartAreas[0].AxisX.Minimum = 0.0;
        Chart1.ChartAreas[0].AxisX.Minimum = freqStart;
        Chart1.ChartAreas[0].AxisX.Maximum = freqEnd;
        Chart1.ChartAreas[0].AxisX.IsLogarithmic = true;
        //Chart1.Series["Current"].ChartArea = "ChartArea2";
    }

    private void Draw2()
    {
       
        DataTable table2;
       
        DataView dView2;

        table2 = new DataTable(); 
        DataColumn column2; 
        DataRow row2; 

        column2 = new DataColumn(); 
        column2.DataType = System.Type.GetType("System.Double"); 
        column2.ColumnName = "Czestotliwosc"; 
        table2.Columns.Add(column2); 

        column2 = new DataColumn(); 
        column2.DataType = System.Type.GetType("System.Double"); 
        column2.ColumnName = "Widmo fazowe";
        table2.Columns.Add(column2); 

        for (int i = 0; i < size; i++)
        {
            row2 = table2.NewRow(); 
            row2["Czestotliwosc"] = Results[i, 0]; 
            row2["Widmo fazowe"] = Results2[i, 0];  
            table2.Rows.Add(row2); 
        }

        dView2 = new DataView(table2);
        Chart2.Series.Clear();

        Chart2.DataBindTable(dView2, "Czestotliwosc");

        Chart2.Series["Widmo fazowe"].ChartType = SeriesChartType.Line;
      
        Chart2.Height = 700;
        Chart2.Width = 1400;
        Chart2.BackColor = System.Drawing.ColorTranslator.FromHtml("#9E9E9E");
        Chart2.ChartAreas[0].BackColor = System.Drawing.Color.Gainsboro;

        Chart2.ChartAreas[0].AxisX.LineWidth = 2;

        Chart2.ChartAreas[0].AxisX.Title = "Częstotliwość [Hz]";
        Chart2.ChartAreas[0].AxisY.Title = "Faza [stopnie]";

        Chart2.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Italic);
        Chart2.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Italic);

        Chart2.Titles.Clear();
        Chart2.Titles.Add("Widmo fazowe");
        Chart2.Series["Widmo fazowe"].BorderWidth = 2;

        Chart2.Series["Widmo fazowe"].Color = System.Drawing.Color.Blue;
        Chart2.Titles[0].Font = new System.Drawing.Font("Arial", 26F, System.Drawing.FontStyle.Regular);

        Chart2.Titles[0].ForeColor = System.Drawing.Color.Black;

        Chart2.Legends[0].DockedToChartArea = Chart1.ChartAreas[0].Name;
         
        Chart2.ChartAreas[0].AxisX.Minimum = freqStart;
        Chart2.ChartAreas[0].AxisX.Maximum = freqEnd;
        Chart2.ChartAreas[0].AxisX.IsLogarithmic = true;    
    }

    private void Draw3()
    {
        
        DataTable table3;
      
        DataView dView3;

        table3 = new DataTable(); 
        DataColumn column3; 
        DataRow row3; 

        column3 = new DataColumn(); 
        column3.DataType = System.Type.GetType("System.Double"); 
        column3.ColumnName = "Czestotliwosc"; 
        table3.Columns.Add(column3); 

        column3 = new DataColumn(); 
        column3.DataType = System.Type.GetType("System.Double"); 
        column3.ColumnName = "Prad"; 
        table3.Columns.Add(column3); 

        //  column3 = new DataColumn(); //inicjalizacja obiektu kolumny
        //  column3.DataType = System.Type.GetType("System.Double"); //zdefiniowanie typu danych w kolumnie
        //  column3.ColumnName = "Odwrotnosc pradu"; //zdefiniowanie nazwy kolumny
        //  table3.Columns.Add(column3); //dodanie kolumny do klasy kolekcji

        for (int i = 0; i < size; i++)
        {
            row3 = table3.NewRow(); 
            row3["Czestotliwosc"] = Results[i, 0]; 
            row3["Prad"] = Results2[i, 1];  
            //  row3["Odwrotnosc pradu"] = -Results2[i, 1];  //druga celka  - kolumna Voltage
            table3.Rows.Add(row3); 
        }
        max3 = Results2[0, 1];
        min3 = Results2[0, 1];

        for (int i = 0; i < size; i++)
        {
            if (Results2[i, 1] > max3) max3 = Results2[i, 1];
            else if (Results2[i, 1] < min3) min3 = Results2[i, 1];
        }

        dView3 = new DataView(table3);
        Chart3.Series.Clear(); 

        Chart3.DataBindTable(dView3, "Czestotliwosc");

        Chart3.Series["Prad"].ChartType = SeriesChartType.Line;
        // Chart3.Series["Odwrotnosc pradu"].ChartType = SeriesChartType.Line;
        // Chart1.Series["Current"].ChartType = SeriesChartType.Line;
        //  Chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{#0.000}"; //ustawienie formatu dla osi X

        Chart3.Height = 700;
        Chart3.Width = 1400;
        Chart3.BackColor = System.Drawing.ColorTranslator.FromHtml("#9E9E9E");

        Chart3.ChartAreas[0].BackColor = System.Drawing.Color.Gainsboro;

        Chart3.ChartAreas[0].AxisX.LineWidth = 2;

        Chart3.ChartAreas[0].AxisX.Title = "Częstotliwość [Hz]";
        Chart3.ChartAreas[0].AxisY.Title = "Prąd zasilający [mA]";

        Chart3.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Italic);
        Chart3.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Italic);
        Chart3.Titles.Clear();
        Chart3.Titles.Add("Obwiednia prądu zasilającego I1");

        Chart3.Series["Prad"].BorderWidth = 2;

        Chart3.Series["Prad"].Color = System.Drawing.Color.Blue;

        Chart3.Titles[0].Font = new System.Drawing.Font("Arial", 26F, System.Drawing.FontStyle.Bold);

        Chart3.Titles[0].ForeColor = System.Drawing.Color.Black;

        Chart3.Legends[0].DockedToChartArea = Chart1.ChartAreas[0].Name;
        //Chart1.Legends[0].Docking = System.Web.UI.DataVisualization.Charting.Docking.Right;

        // Chart1.ChartAreas[0].AxisX.Minimum = 0.0;
        Chart3.ChartAreas[0].AxisX.Minimum = freqStart;
        Chart3.ChartAreas[0].AxisX.Maximum = freqEnd;

        Chart3.ChartAreas[0].AxisY.Minimum = min3;
        Chart3.ChartAreas[0].AxisY.Maximum = max3;
        Chart3.ChartAreas[0].AxisY.LabelStyle.Format = "{#0.00}";

        Debug.WriteLine(min3.ToString());
        Debug.WriteLine(max3.ToString());

        Chart3.ChartAreas[0].AxisX.IsLogarithmic = true;

        //Chart1.Series["Current"].ChartArea = "ChartArea2";
    }

    protected void btn1_Click(object sender, EventArgs e)
    {

        MultiView1.ActiveViewIndex = 0;
        Calculate();

        if (IntegrityCheck() == 1)
        {
            if (dropDownList2.SelectedValue.ToString() == "1")
            {
                if (freqEnd > 20000000)
                {
                    clearChartSpace();
                    error = "częstotliwości końcowej. Częstotliwość przekracza wartość 20MHz";
                    labMessage.Text = dateAndTime + "Błędna wartość częstotliwości końcowej.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Proszę podaj poprawne wartosci - błąd wartości " + error + "');</script>");
                }
                else
                {
                    Draw();
                    labMessage.Text = dateAndTime + "Poprawnie wygenerowano widmo amplitudowe.";
                }
            }

            if (dropDownList2.SelectedValue.ToString() == "2")
            {
                if (freqEnd > 20000)
                {
                    clearChartSpace();
                    error = "częstotliwości końcowej. Częstotliwość przekracza wartość 20MHz";
                    labMessage.Text = dateAndTime + "Błędna wartość częstotliwości końcowej.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Proszę podaj poprawne wartosci - błąd wartości " + error + "');</script>");
                }
                else
                {
                    Draw();
                    labMessage.Text = dateAndTime + "Poprawnie wygenerowano widmo amplitudowe.";
                }
            }

            if (dropDownList2.SelectedValue.ToString() == "3")
            {
                if (freqEnd > 20)
                {
                    clearChartSpace();
                    error = "częstotliwości końcowej. Częstotliwość przekracza wartość 20MHz";
                    labMessage.Text = dateAndTime + "Błędna wartość częstotliwości końcowej.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Proszę podaj poprawne wartosci - błąd wartości " + error + "');</script>");
                }
                else
                {
                    Draw();
                    labMessage.Text = dateAndTime + "Poprawnie wygenerowano widmo amplitudowe.";
                }
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Proszę podaj poprawne wartosci - błąd wartości " + error + "');</script>");
            panWaveform.Visible = false;
            panWaveform2.Visible = false;
            panWaveform3.Visible = false;
        }

    }

    protected void btn2_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        Calculate();
        if (IntegrityCheck() == 1)
        {
            if (dropDownList2.SelectedValue.ToString() == "1")
            {
                if (freqEnd > 20000000)
                {
                    clearChartSpace();
                    error = "częstotliwości końcowej. Częstotliwość przekracza wartość 20MHz";
                    labMessage.Text = dateAndTime + "Błędna wartość częstotliwości końcowej.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Proszę podaj poprawne wartosci - błąd wartości " + error + "');</script>");
                }
                else
                {
                    Draw2();
                    labMessage.Text = dateAndTime + "Poprawnie wygenerowano widmo fazowe.";
                }
            }

            if (dropDownList2.SelectedValue.ToString() == "2")
            {
                if (freqEnd > 20000)
                {
                    clearChartSpace();
                    error = "częstotliwości końcowej. Częstotliwość przekracza wartość 20MHz";
                    labMessage.Text = dateAndTime + "Błędna wartość częstotliwości końcowej.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Proszę podaj poprawne wartosci - błąd wartości " + error + "');</script>");
                }
                else
                {
                    Draw2();
                    labMessage.Text = dateAndTime + "Poprawnie wygenerowano widmo fazowe.";
                }
            }

            if (dropDownList2.SelectedValue.ToString() == "3")
            {
                if (freqEnd > 20)
                {
                    clearChartSpace();
                    error = "częstotliwości końcowej. Częstotliwość przekracza wartość 20MHz";
                    labMessage.Text = dateAndTime + "Błędna wartość częstotliwości końcowej.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Proszę podaj poprawne wartosci - błąd wartości " + error + "');</script>");
                }
                else
                {
                    Draw2();
                    labMessage.Text = dateAndTime + "Poprawnie wygenerowano widmo fazowe.";
                }
            }
            }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Proszę podaj poprawne wartosci - błąd wartości " + error + "');</script>");
            panWaveform.Visible = false;
            panWaveform2.Visible = false;
            panWaveform3.Visible = false;
        }
    }

    protected void btn3_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 2;
        Calculate();
        if (IntegrityCheck() == 1)
        {
            if (dropDownList2.SelectedValue.ToString() == "1")
            {
                if (freqEnd > 20000000)
                {
                    clearChartSpace();
                    error = "częstotliwości końcowej. Częstotliwość przekracza wartość 20MHz";
                    labMessage.Text = dateAndTime + "Błędna wartość częstotliwości końcowej.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Proszę podaj poprawne wartosci - błąd wartości " + error + "');</script>");
                }
                else
                {
                    Draw3();
                    labMessage.Text = dateAndTime + "Poprawnie wygenerowano obwiednie prądu zasilającego.";
                }
            }

            if (dropDownList2.SelectedValue.ToString() == "2")
            {
                if (freqEnd > 20000)
                {
                    clearChartSpace();
                    error = "częstotliwości końcowej. Częstotliwość przekracza wartość 20MHz";
                    labMessage.Text = dateAndTime + "Błędna wartość częstotliwości końcowej.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Proszę podaj poprawne wartosci - błąd wartości " + error + "');</script>");
                }
                else
                {
                    Draw3();
                    labMessage.Text = dateAndTime + "Poprawnie wygenerowano obwiednie prądu zasilającego.";
                }
            }

            if (dropDownList2.SelectedValue.ToString() == "3")
            {
                if (freqEnd > 20)
                {
                    clearChartSpace();
                    error = "częstotliwości końcowej. Częstotliwość przekracza wartość 20MHz";
                    labMessage.Text = dateAndTime + "Błędna wartość częstotliwości końcowej.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Proszę podaj poprawne wartosci - błąd wartości " + error + "');</script>");
                }
                else
                {
                    Draw3();
                    labMessage.Text = dateAndTime + "Poprawnie wygenerowano obwiednie prądu zasilającego.";
                }
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Proszę podaj poprawne wartosci - błąd wartości " + error + "');</script>");
            panWaveform.Visible = false;
            panWaveform2.Visible = false;
            panWaveform3.Visible = false;
        }
    }

    protected void Credits_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Projekt stworzony przez: BC');</script>");
        panWaveform.Visible = false;
        panWaveform2.Visible = false;
        panWaveform3.Visible = false;
    }

    private int IntegrityCheck()
    {
        if (Am > 0 && fzas > 0 && C > 0 && L > 0 && R1 > 0 && R2 > 0 && freqStart > 0 && freqEnd > 0 && size > 999 && size<=170000)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtMagnitude.Text = "";
        txtFreqEnd.Text = "";
        txtFreqStart.Text = "";
        txtInd.Text = "";
        txtFrequency.Text = "";
        txtRes1.Text = "";
        txtRes2.Text = "";
        txtCap.Text = "";
        txtSamples.Text = "";

        getCurrentTimeandDate();
        labMessage.Text = dateAndTime + "Wyczyszczono parametry";

        panWaveform.Visible = false;
        panWaveform2.Visible = false;
        panWaveform3.Visible = false;
    }

    protected void clearChartSpace()
    {
        panWaveform.Visible = false;
        panWaveform2.Visible = false;
        panWaveform3.Visible = false;
    }

    protected void getCurrentTimeandDate()
    {
        DateTime dateTime = DateTime.UtcNow.Date;
        dateAndTime = dateTime.ToString("d") + " " + DateTime.Now.ToString("HH:mm:ss") + " ";
    }

    protected void Credits2_Click(object sender, EventArgs e)
    {
        Response.Redirect("proofOfConcept.aspx");
    }
}