using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HelixToolkit;
using HelixToolkit.Wpf;
using System.Windows.Forms.DataVisualization.Charting;
using NMRMBC;
using System.IO;
namespace NMRMBCviewer
{
    public partial class Form1 : Form
    {
        string[] directorios;
        string[] experimentos;
        string filepath;
        NMRMBC.new_spectrum ns = new NMRMBC.new_spectrum();
        NMRMBC.read rd = new NMRMBC.read();
        NMRMBC.diffusion diffusion = new NMRMBC.diffusion();
        NMRMBC.espectrum espectro = new NMRMBC.espectrum();
        NMRMBC.diffusion difusion = new NMRMBC.diffusion();
        private double SelectionStart = double.NaN;
        bool start = false;
        private void Create3DViewPort()
        {
            var hVp3D = new HelixViewport3D();
            var lights = new DefaultLights();
            var teaPot = new Teapot();
            hVp3D.Children.Add(lights);
            hVp3D.Children.Add(teaPot);
            //this.AddChild(hVp3D);
        } 
        
        public Form1()
        {
            InitializeComponent();
            Create3DViewPort();
        }

       
        private void Form1_Load(object sender, EventArgs e)
        {

            
            string font = "Segoe UI";
            float fontpoint = 20;
            FontStyle fontStyle = FontStyle.Regular;

            // Set axis title
            chart1.ChartAreas[0].AxisX.Title = "ppm";

            // Set Title font
            chart1.ChartAreas[0].AxisX.TitleFont = new Font(font, fontpoint, fontStyle);
            // Set Title color
            chart1.ChartAreas[0].AxisX.TitleForeColor = Color.Black;

            // Set axis title
            chart1.ChartAreas[0].AxisY.Title = "Intensity (10^6)";

            // Set Title font
            chart1.ChartAreas[0].AxisY.TitleFont = new Font(font, fontpoint, fontStyle);
           
           
            //// Set Title color
            //string font = "Segoe UI";
            //float fontpoint = 20;
            //FontStyle fontStyle = FontStyle.Regular;
            //chart1.ChartAreas[0].AxisY.TitleForeColor = Color.Black;
            //chart1.Titles.Add("NMRMBC Spectrum Viewer");
            //chart1.Titles[0].Font = new Font(font, fontpoint, fontStyle);
            this.chart1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chart1_KeyUp);
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
           
            chart1.ChartAreas[0].IsSameFontSizeForAllAxes = true;       
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            // Avoid dialog processing of arrow keys
            if (keyData == Keys.Left || keyData == Keys.Right)
            {
                return false;
            }
            return base.ProcessDialogKey(keyData);
        }
        private void ProcessSelect(System.Windows.Forms.KeyEventArgs e)
        {
            // Process keyboard keys
            if (e.KeyCode == Keys.Right)
            {
                // Make sure the selection start value is assigned
                if (this.SelectionStart == double.NaN) { 
                    this.SelectionStart = chart1.ChartAreas[0].CursorX.Position;
                    this.SelectionStart = chart1.ChartAreas[0].CursorY.Position;
                }
                // Set the new cursor position 
                chart1.ChartAreas[0].CursorX.Position += chart1.ChartAreas[0].CursorX.Interval;
                chart1.ChartAreas[0].CursorX.Position += chart1.ChartAreas[0].CursorY.Interval;
            }
            else if (e.KeyCode == Keys.Left)
            {
                // Make sure the selection start value is assigned
                if (this.SelectionStart == double.NaN) { 
                    this.SelectionStart = chart1.ChartAreas[0].CursorX.Position;
                    this.SelectionStart = chart1.ChartAreas[0].CursorY.Position;
                }

                // Set the new cursor position 
                chart1.ChartAreas[0].CursorX.Position -= chart1.ChartAreas[0].CursorX.Interval;
                chart1.ChartAreas[0].CursorX.Position -= chart1.ChartAreas[0].CursorY.Interval;
            }

            // If the cursor is outside the view, set the view
            // so that the cursor can be seen
            SetView();


            chart1.ChartAreas[0].CursorX.SelectionStart = this.SelectionStart;
            chart1.ChartAreas[0].CursorX.SelectionEnd = chart1.ChartAreas[0].CursorX.Position;
            chart1.ChartAreas[0].CursorX.SelectionEnd = chart1.ChartAreas[0].CursorY.Position;
        }
        private void SetView()
        {
            // Keep the cursor from leaving the max and min axis points
            if (chart1.ChartAreas[0].CursorX.Position < 0){
                chart1.ChartAreas[0].CursorX.Position = 0;
              
            }
            else if (chart1.ChartAreas[0].CursorX.Position > 75) { 
                chart1.ChartAreas[0].CursorX.Position = 75;
               
            }

            // Move the view to keep the cursor visible
            if (chart1.ChartAreas[0].CursorX.Position < chart1.ChartAreas[0].AxisX.ScaleView.Position){
                chart1.ChartAreas[0].AxisX.ScaleView.Position = chart1.ChartAreas[0].CursorX.Position;
               
            }
            else if ((chart1.ChartAreas[0].CursorX.Position >
                (chart1.ChartAreas[0].AxisX.ScaleView.Position + chart1.ChartAreas[0].AxisX.ScaleView.Size)))
            {
                chart1.ChartAreas[0].AxisX.ScaleView.Position =
                    (chart1.ChartAreas[0].CursorX.Position - chart1.ChartAreas[0].AxisX.ScaleView.Size);
            }
            // Keep the cursor from leaving the max and min axis points
            if (chart1.ChartAreas[0].CursorY.Position < 0)
            {
                chart1.ChartAreas[0].CursorY.Position = 0;

            }
            else if (chart1.ChartAreas[0].CursorY.Position > 75)
            {
                chart1.ChartAreas[0].CursorY.Position = 75;

            }

            // Move the view to keep the cursor visible
            if (chart1.ChartAreas[0].CursorY.Position < chart1.ChartAreas[0].AxisY.ScaleView.Position)
            {
                chart1.ChartAreas[0].AxisY.ScaleView.Position = chart1.ChartAreas[0].CursorY.Position;

            }
            else if ((chart1.ChartAreas[0].CursorY.Position >
                (chart1.ChartAreas[0].AxisY.ScaleView.Position + chart1.ChartAreas[0].AxisY.ScaleView.Size)))
            {
                chart1.ChartAreas[0].AxisY.ScaleView.Position =
                    (chart1.ChartAreas[0].CursorY.Position - chart1.ChartAreas[0].AxisY.ScaleView.Size);
            }
        }
        private void ProcessScroll(System.Windows.Forms.KeyEventArgs e)
        {
            // Process keyboard keys
            if (e.KeyCode == Keys.Right)
                // set the new cursor position 
                chart1.ChartAreas[0].CursorX.Position += chart1.ChartAreas[0].CursorX.Interval;

            else if (e.KeyCode == Keys.Left)
                // Set the new cursor position 
                chart1.ChartAreas[0].CursorX.Position -= chart1.ChartAreas[0].CursorX.Interval;

            // If the cursor is outside the view, set the view
            // so that the cursor can be seen
            SetView();


            // Set the selection start variable in case shift arrows are selected
            this.SelectionStart = chart1.ChartAreas[0].CursorX.Position;
            this.SelectionStart = chart1.ChartAreas[0].CursorY.Position;
            // Reset the old selection start and end
            chart1.ChartAreas[0].CursorX.SelectionStart = double.NaN;
            chart1.ChartAreas[0].CursorX.SelectionEnd = double.NaN;
            chart1.ChartAreas[0].CursorY.SelectionStart = double.NaN;
            chart1.ChartAreas[0].CursorY.SelectionEnd = double.NaN;
        }

        private void chart1_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {

            if ((e.KeyCode == Keys.Right) || (e.KeyCode == Keys.Left))
            {
                // If the key event is shifted, process as a selection
                if (e.Shift)
                    ProcessSelect(e);
                else // Process as a scroll
                    ProcessScroll(e);
            }

            // On enter, zoom the selection
            else if (e.KeyCode == Keys.Enter)
            {
                double start, end;

                if (chart1.ChartAreas[0].CursorX.SelectionStart > chart1.ChartAreas[0].CursorX.SelectionEnd)
                {
                    start = chart1.ChartAreas[0].CursorX.SelectionEnd;
                    end = chart1.ChartAreas[0].CursorX.SelectionStart;
                }
                else
                {
                    end = chart1.ChartAreas[0].CursorX.SelectionEnd;
                    start = chart1.ChartAreas[0].CursorX.SelectionStart;
                }
                if (chart1.ChartAreas[0].CursorY.SelectionStart > chart1.ChartAreas[0].CursorY.SelectionEnd)
                {
                    start = chart1.ChartAreas[0].CursorY.SelectionEnd;
                    end = chart1.ChartAreas[0].CursorY.SelectionStart;
                }
                else
                {
                    end = chart1.ChartAreas[0].CursorY.SelectionEnd;
                    start = chart1.ChartAreas[0].CursorY.SelectionStart;
                }

                // Return if no selection actually made
                if (start == end)
                    return;

                // Zoom the selection
                chart1.ChartAreas[0].AxisX.ScaleView.Zoom(start, (end - start), DateTimeIntervalType.Number, true);
                chart1.ChartAreas[0].AxisY.ScaleView.Zoom(start, (end - start), DateTimeIntervalType.Number, true);
                // Reset selection values
                this.SelectionStart = chart1.ChartAreas[0].CursorX.Position;
                chart1.ChartAreas[0].CursorX.SelectionStart = double.NaN;
                chart1.ChartAreas[0].CursorX.SelectionEnd = double.NaN;
                this.SelectionStart = chart1.ChartAreas[0].CursorY.Position;
                chart1.ChartAreas[0].CursorY.SelectionStart = double.NaN;
                chart1.ChartAreas[0].CursorY.SelectionEnd = double.NaN;

                if (chart1.ChartAreas[0].CursorY.SelectionStart > chart1.ChartAreas[0].CursorY.SelectionEnd)
                {
                    start = chart1.ChartAreas[0].CursorY.SelectionEnd;
                    end = chart1.ChartAreas[0].CursorY.SelectionStart;
                }
                else
                {
                    end = chart1.ChartAreas[0].CursorY.SelectionEnd;
                    start = chart1.ChartAreas[0].CursorY.SelectionStart;
                }
                if (chart1.ChartAreas[0].CursorX.SelectionStart > chart1.ChartAreas[0].CursorX.SelectionEnd)
                {
                    start = chart1.ChartAreas[0].CursorX.SelectionEnd;
                    end = chart1.ChartAreas[0].CursorX.SelectionStart;
                }
                else
                {
                    end = chart1.ChartAreas[0].CursorX.SelectionEnd;
                    start = chart1.ChartAreas[0].CursorX.SelectionStart;
                }
                // Return if no selection actually made
                if (start == end)
                    return;

                // Zoom the selection
                chart1.ChartAreas[0].AxisY.ScaleView.Zoom(start, (end - start), DateTimeIntervalType.Number, true);

                // Reset selection values
                this.SelectionStart = chart1.ChartAreas[0].CursorY.Position;
                chart1.ChartAreas[0].CursorY.SelectionStart = double.NaN;
                chart1.ChartAreas[0].CursorY.SelectionEnd = double.NaN;
            }

            else if (e.KeyCode == Keys.Back)
            {
                // Reset zoom back to previous view state
                chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset(1);
                chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset(1);
                // Reset selection values
                this.SelectionStart = chart1.ChartAreas[0].CursorX.Position;
                chart1.ChartAreas[0].CursorX.SelectionStart = double.NaN;
                chart1.ChartAreas[0].CursorX.SelectionEnd = double.NaN;
                this.SelectionStart = chart1.ChartAreas[0].CursorY.Position;
                chart1.ChartAreas[0].CursorY.SelectionStart = double.NaN;
                chart1.ChartAreas[0].CursorY.SelectionEnd = double.NaN;
            }

        }

        private void chart1_Click(object sender, System.EventArgs e)
        {
            // Set input focus to the chart control
            chart1.Focus();

            // Set the selection start variable to that of the current position
            this.SelectionStart = chart1.ChartAreas[0].CursorX.Position;
            this.SelectionStart = chart1.ChartAreas[0].CursorY.Position;
        }
        private void nuevoToolStripButton_Click(object sender, EventArgs e)
        {
            diffusion.clear();
            espectro.clear();
            chart1.Series.Clear();
            treeView1.Nodes.Clear();
            start = false;
            directorios = null;
            experimentos = null;
            filepath = null;
            chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset(0);
            chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset(0);
            chart1.Titles.Clear();
        }
        private void loadTree()
        {

            TreeNode tvRoot;
            TreeNode tvNode;
            TreeNode tv;
            string[] separar;
            separar = filepath.Split(new Char[] { '\\' });
            tvRoot = this.treeView1.Nodes.Add(separar[separar.Length - 1]);
            foreach (string st in experimentos)
            {
                tvNode = tvRoot.Nodes.Add(st);
            }

            tv = treeView1.Nodes[0];
            //// Set Title color
            string font = "Segoe UI";
            float fontpoint = 20;
            FontStyle fontStyle = FontStyle.Regular;
            chart1.ChartAreas[0].AxisY.TitleForeColor = Color.Black;
            chart1.Titles.Add(tvRoot.Text);
            chart1.Titles[0].Font = new Font(font, fontpoint, fontStyle);

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void treeView1_Click(object sender, EventArgs e)
        {
            
            
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            
            TreeNode tvNode;
            tvNode = e.Node;
            bool bl = false;
           
            foreach (string st in experimentos)
            { 
                
                if (st == tvNode.Text)
                {
                    bl = true;
                }
            }

            if (bl)
            {
                rd.read_param(filepath + "\\" + tvNode.Text + "\\" + "pdata\\1\\procs");
                rd.readReal(filepath, tvNode.Text);
                espectro = rd.ESPECTRUM;
                Series serie = new Series();
                if (diffusion.add(tvNode.Text, espectro))
                { 
                                             
                    serie.LegendText = tvNode.Text;
                    serie.ChartType = SeriesChartType.Line;

                    serie.IsVisibleInLegend = true;
               

                    for(int i=1;i<=(espectro.Count-1);i++)
                    {
                        coordenadas cd = new coordenadas();
                        cd =espectro[Convert.ToString(i)];
                        double x;                      
                        x = -cd.X;
                        x = Math.Round(x,5);
                        serie.Points.AddXY(x,cd.Y/1000000);

                    }
                
                    serie.ChartArea = chart1.ChartAreas[0].Name;
                    chart1.Series.Add(serie);
                    // Set automatic zooming
                    chart1.ChartAreas[chart1.ChartAreas[0].Name].AxisX.ScaleView.Zoomable = true;
                    chart1.ChartAreas[chart1.ChartAreas[0].Name].AxisY.ScaleView.Zoomable = true;

                    // Set automatic scrolling 
                    chart1.ChartAreas[chart1.ChartAreas[0].Name].CursorX.AutoScroll = true;
                    chart1.ChartAreas[chart1.ChartAreas[0].Name].CursorY.AutoScroll = true;
                    var ca = chart1.ChartAreas[chart1.ChartAreas[0].Name].CursorX;
                    ca.IsUserEnabled = true;
                    ca.IsUserSelectionEnabled = true;
                    ca = chart1.ChartAreas[chart1.ChartAreas[0].Name].CursorY;
                    ca.IsUserEnabled = true;
                    ca.IsUserSelectionEnabled = true;
                    chart1.Update();
                    // Show as 3D
                    //chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
                    //chart1.ChartAreas[0].Area3DStyle.IsClustered = true;
                    //chart1.ChartAreas[0].Area3DStyle.Rotation = 45;
                    //chart1.ChartAreas[0].Area3DStyle.Inclination = 45;
                
                    bl = false;
                }
            }
            if (diffusion.Count == 1)
            {
               
            }
                            
        }

        private void cortarToolStripButton_Click(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset(0);
            chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset(0);
        }

        private void abrirToolStripButton_Click(object sender, EventArgs e)
        {
            
            start = ns.read_directories();

            if (start)
            {

                experimentos = ns.experimentos;
                directorios = ns.directorio;
                filepath = ns.filepath;
                loadTree();

            }
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            diffusion.clear();
            espectro.clear();
            chart1.Series.Clear();
            treeView1.Nodes.Clear();
            start = false;
            directorios = null;
            experimentos = null;
            filepath = null;
            chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset(0);
            chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset(0);
            chart1.Titles.Clear();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            start = ns.read_directories();

            if (start)
            {

                experimentos = ns.experimentos;
                directorios = ns.directorio;
                filepath = ns.filepath;
                loadTree();

            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guardarToolStripButton_Click(object sender, EventArgs e)
        {
            if (start)
            { 
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                    saveFileDialog1.Filter = "txt files (*.txt)|*.txt|xml files (*.xml)|*.XML";
                    saveFileDialog1.FilterIndex = 1;
                    saveFileDialog1.RestoreDirectory = true;
                    saveFileDialog1.Title = "Save Spectrums";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        try 
                        {
                            switch (saveFileDialog1.FilterIndex)
                            {
                                case 1:
                                    string[] split;
                                    split = saveFileDialog1.FileName.Split(new Char[] { '.' });

                                    foreach (Series sr in chart1.Series)
                                    {

                                        System.IO.StreamWriter sw = new System.IO.StreamWriter(split[0] + chart1.Titles[0].Text + "_" + sr.LegendText + "." + split[1]);
                                        sw.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
                                        sw.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
                                        sw.WriteLine("%Collections of spectrum. File " + split[0] + sr.LegendText + "." + split[1] + "                      %");
                                        sw.WriteLine("%Developed by Francisco Manuel Arrabal Campos member of NMRMBC group research at University of Almeria%");
                                        sw.WriteLine("%Follow us in www2.ual.es/NMRMBC/ a look into for new researches and much more.                        %");
                                        sw.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
                                        sw.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
                                        sw.WriteLine("%First data is ppm and the second data is Intensity of the signal detected, so the estructure is X    Y%");
                                        sw.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
                                        sw.WriteLine("%X    Y                                                                                                  %");
                                        int cont = 0;
                                        foreach (DataPoint pt in sr.Points)
                                        {
                                            double x, y;
                                            x = pt.XValue;
                                            y = pt.YValues[0] * 1000000;
                                            sw.WriteLine(x.ToString() + ";" + y.ToString());
                                            cont++;
                                        }
                                        sw.Close();
                                    }


                                    break;

                                case 2:

                                    string[] separar;
                                    DataSet ds;
                                    foreach (Series sr in chart1.Series)
                                    {

                                        separar = saveFileDialog1.FileName.Split(new Char[] { '.' });
                                        ds = chart1.DataManipulator.ExportSeriesValues(sr.Name);
                                        ds.WriteXml(separar[0] + sr.LegendText + "." + separar[1]);

                                    }
                                    Series sr0 = chart1.Series[0];
                                    ds = chart1.DataManipulator.ExportSeriesValues(sr0.Name);

                                    separar = saveFileDialog1.FileName.Split(new Char[] { '.' });
                                    ds.WriteXmlSchema(separar[0] + "_schema." + separar[1]);


                                    break;
                            }
                            MessageBox.Show("Files was saved");
                        }
                        catch
                        {
                            MessageBox.Show("Some happend worng!!!, try again");
                        
                        }
                       
                    }
            }
        }

        private void imprimirToolStripButton_Click(object sender, EventArgs e)
        {
            if (start)
            {
                string font = "Segoe UI";
                float fontpoint = 20;
                FontStyle fontStyle = FontStyle.Regular;
                chart1.Titles.Add(DateTime.Today.ToShortDateString());
                chart1.ChartAreas[0].AxisY.TitleForeColor = Color.Black;
                chart1.Titles[0].Font = new Font(font, fontpoint, fontStyle);
                chart1.Titles.Add("Advanced NMR methods and Metal-base catalysts");
                chart1.Printing.PrintPreview();
            }
        }

        private void vistapreviadeimpresiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (start)
            {
                string font = "Segoe UI";
                float fontpoint = 20;
                FontStyle fontStyle = FontStyle.Regular;
                chart1.Titles.Add(DateTime.Today.ToShortDateString());
                chart1.ChartAreas[0].AxisY.TitleForeColor = Color.Black;
                chart1.Titles[0].Font = new Font(font, fontpoint, fontStyle);
                chart1.Titles.Add("Advanced NMR methods and Metal-base catalysts");
                chart1.Printing.PrintPreview();
            }
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (start) { 
                string font = "Segoe UI";
                float fontpoint = 20;
                FontStyle fontStyle = FontStyle.Regular;
                chart1.Titles.Add(DateTime.Today.ToShortDateString());
                chart1.ChartAreas[0].AxisY.TitleForeColor = Color.Black;
                chart1.Titles[0].Font = new Font(font, fontpoint, fontStyle);
                chart1.Titles.Add("Advanced NMR methods and Metal-base catalysts");
                chart1.Printing.Print(true);
            }

        }

        private void copiarToolStripButton_Click(object sender, EventArgs e)
        {
            // create a memory stream to save the chart image    
            System.IO.MemoryStream stream = new System.IO.MemoryStream();

            // save the chart image to the stream    
            chart1.SaveImage(stream, System.Drawing.Imaging.ImageFormat.Bmp);

            // create a bitmap using the stream    
            Bitmap bmp = new Bitmap(stream);

            // save the bitmap to the clipboard    
            Clipboard.SetDataObject(bmp); 
        }

        private void acercadeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

       

      
    }
}
