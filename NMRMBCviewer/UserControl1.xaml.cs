using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HelixToolkit.Wpf;
using HelixToolkit;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.ComponentModel;
using System.Linq;


namespace NMRMBCviewer
{
    /// <summary>
    /// Lógica de interacción para UserControl1.xaml
    /// </summary>
    /// 
    
    public partial class UserControl1 : UserControl
    {
       
        public UserControl1()
        {
            InitializeComponent();
            Create3DViewPort();
            //this.DataContext = new MainViewModel();
        }
        private void Create3DViewPort()
        {

            //viewport.DefaultCamera = new PerspectiveCamera();
            //viewport.DefaultCamera.Position = new Point3D(300, 300, 300);
            //viewport.DefaultCamera.LookDirection = new Vector3D(-200, -200, -200);
            //viewport.DefaultCamera.UpDirection = new Vector3D(0, 0, 1);
            //DefaultLights light = new DefaultLights();
            //viewport.Children.Add(light);
            //viewport.Children.Add(new GridLinesVisual3D());
            //SphereVisual3D sphere = new SphereVisual3D();
            
            //sphere.Radius = 5;
            //sphere.Center = new Point3D(20, 20, 20);
            //sphere.Fill = Brushes.Red;
            //viewport.Children.Add(sphere);
            
            
        }
        public bool addGraph(NMRMBC.diffusion df)
        {
            this.DataContext = new MainViewModel(df);
            return true;
        }
    }
    //public enum ColorCoding
    //{
    //    /// <summary>
    //    /// No color coding, use coloured lights
    //    /// </summary>
    //    ByLights,

    //    /// <summary>
    //    /// Color code by gradient in y-direction using a gradient brush with white ambient light
    //    /// </summary>
    //    ByGradientY
    //}

    //public class MainViewModel : INotifyPropertyChanged
    //{
    //    double _MinX, _MinY, _MaxX, _MaxY;
    //    int _Rows, _Columns;
    //    NMRMBC.diffusion difusion;
    //    NMRMBC.espectrum espectro;
    //    public double MinX { get { return _MinX; } set {_MinX= value ;} }
    //    public double MinY { get { return _MinY; } set { _MinY = value; } }
    //    public double MaxX { get { return _MaxX; } set { _MaxX = value; } }
    //    public double MaxY { get { return _MaxY; } set { _MaxY = value; } }
    //    public int Rows { get { return _Rows; } set { _Rows = value; } }
    //    public int Columns { get { return _Columns; } set { _Columns = value; } }

    //    public Func<double, double, double> Function { get; set; }
    //    public Point3D[,] Data { get; set; }
    //    public double[,] ColorValues { get; set; }

    //    public ColorCoding ColorCoding { get; set; }

    //    public Model3DGroup Lights
    //    {
    //        get
    //        {
    //            var group = new Model3DGroup();
    //            switch (ColorCoding)
    //            {
    //                case ColorCoding.ByGradientY:
    //                    group.Children.Add(new AmbientLight(Colors.White));
    //                    break;
    //                case ColorCoding.ByLights:
    //                    group.Children.Add(new AmbientLight(Colors.Gray));
    //                    group.Children.Add(new PointLight(Colors.Red, new Point3D(0, -1000, 0)));
    //                    group.Children.Add(new PointLight(Colors.Blue, new Point3D(0, 0, 1000)));
    //                    group.Children.Add(new PointLight(Colors.Green, new Point3D(1000, 1000, 0)));
    //                    break;
    //            }
    //            return group;
    //        }
    //    }

    //    public Brush SurfaceBrush
    //    {
    //        get
    //        {
    //            // Brush = BrushHelper.CreateGradientBrush(Colors.White, Colors.Blue);
    //            // Brush = GradientBrushes.RainbowStripes;
    //            // Brush = GradientBrushes.BlueWhiteRed;
    //            switch (ColorCoding)
    //            {
    //                case ColorCoding.ByGradientY:
    //                    return BrushHelper.CreateGradientBrush(Colors.Red, Colors.White, Colors.Blue);
    //                case ColorCoding.ByLights:
    //                    return Brushes.White;
    //            }
    //            return null;
    //        }
    //    }
    //    public void establishment_diffusion(NMRMBC.diffusion df)
    //    {

    //        difusion = df;
    //    }
    //    public MainViewModel()
    //    {

    //        //NMRMBC.diffusion diff
    //        this.MinX = -100;
    //        this.MaxX = 100;
    //        this.MinY = -100;
    //        this.MaxY = 100;
    //        this.Rows = 91;
    //        this.Columns = 91;

    //        Function = (x, y) => Math.Cos(x * y) * 1;
    //        //ColorCoding = ColorCoding.ByGradientY;
    //        ColorCoding = ColorCoding.ByLights;
    //        UpdateModel();
    //    }
    //    private void UpdateModel()
    //    {
    //        Data = CreateDataArray(Function);
    //        switch (ColorCoding)
    //        {
    //            case ColorCoding.ByGradientY:
    //                ColorValues = FindGradientY(Data);
    //                break;
    //            case ColorCoding.ByLights:
    //                ColorValues = null;
    //                break;
    //        }
    //        RaisePropertyChanged("Data");
    //        RaisePropertyChanged("ColorValues");
    //        RaisePropertyChanged("SurfaceBrush");
    //    }
    //    public Point GetPointFromIndex(int i, int j)
    //    {
    //        double x = this.MinX + (double)j / (this.Columns - 1) * (this.MaxX - this.MinX);
    //        double y = this.MinY + (double)i / (this.Rows - 1) * (this.MaxY - this.MinY);
    //        return new Point(x, y);
    //    }

    //    public Point3D[,] CreateDataArray(Func<double, double, double> f)
    //    {
    //        var data = new Point3D[this.Rows, this.Columns];
    //        for (int i = 0; i < this.Rows; i++)
    //            for (int j = 0; j < this.Columns; j++)
    //            {
    //                var pt = GetPointFromIndex(i, j);
    //                data[i, j] = new Point3D(pt.X, pt.Y, f(pt.X, pt.Y));
    //            }
    //        return data;
    //    }
    //    //public Point3D[,] CreateDataArray(NMRMBC.diffusion df)
    //    //{
    //    //    NMRMBC.espectrum es;
    //    //    es = df["10"];
    //    //    difusion = df;
    //    //    var data = new Point3D[df.Count,es.Count];
    //    //    for (int i = 0; i < df.Count - 1; i++)
    //    //        if (i == 1)
    //    //        {
    //    //            es = df["20"];
    //    //        }
    //    //        if (i == 2)
    //    //        {
    //    //            es = df["30"];
    //    //        }
    //    //        for (int j = 0; j < es.Count; j++)
    //    //        {
                    
    //    //            //var pt = GetPointFromIndex(i, j);
    //    //            data[i, j] = new Point3D(es[Convert.ToString(j)].X, 10.0, es[Convert.ToString(j)].Y);
    //    //        }
    //    //    return data;
    //    //}
    //    public double[,] FindGradientY(Point3D[,] data)
    //    {
    //        int n = data.GetUpperBound(0) + 1;
    //        int m = data.GetUpperBound(0) + 1;
    //        var K = new double[n, m];
    //        for (int i = 0; i < n; i++)
    //            for (int j = 0; j < m; j++)
    //            {
    //                // Finite difference approximation
    //                var p10 = data[i + 1 < n ? i + 1 : i, j - 1 > 0 ? j - 1 : j];
    //                var p00 = data[i - 1 > 0 ? i - 1 : i, j - 1 > 0 ? j - 1 : j];
    //                var p11 = data[i + 1 < n ? i + 1 : i, j + 1 < m ? j + 1 : j];
    //                var p01 = data[i - 1 > 0 ? i - 1 : i, j + 1 < m ? j + 1 : j];

    //                //double dx = p01.X - p00.X;
    //                //double dz = p01.Z - p00.Z;
    //                //double Fx = dz / dx;

    //                double dy = p10.Y - p00.Y;
    //                double dz = p10.Z - p00.Z;

    //                K[i, j] = dz / dy;
    //            }
    //        return K;
    //    }
    //    public event PropertyChangedEventHandler PropertyChanged;

    //    protected void RaisePropertyChanged(string property)
    //    {
    //        var handler = PropertyChanged;
    //        if (handler != null)
    //        {
    //            handler(this, new PropertyChangedEventArgs(property));
    //        }
    //    }
    
    //}

    public class MainViewModel : INotifyPropertyChanged
    {
        public Point3D[] Data { get; set; }

        public double[] Values { get; set; }

        public Model3DGroup Lights
        {
            get
            {
                var group = new Model3DGroup();
                group.Children.Add(new AmbientLight(Colors.White));
                return group;
            }
        }

        public Brush SurfaceBrush
        {
            get
            {
                // return BrushHelper.CreateGradientBrush(Colors.White, Colors.Blue);
                return GradientBrushes.RainbowStripes;
                // return GradientBrushes.BlueWhiteRed;
            }
        }

        public MainViewModel(NMRMBC.diffusion diff)
        {
            UpdateModel(diff);
        }

        private void UpdateModel(NMRMBC.diffusion diff)
        {
            NMRMBC.espectrum es;
            es = diff["10"];
             //Data = CreateDataArray(diff);
           //Data = Enumerable.Range(0, 7 * 7 * 7).Select(i => new Point3D(i % 7, (i % 49) / 7, i / 49)).ToArray();
            //Data = Enumerable.Range(0, es.Count * 3 * es.Count).Select(i => new Point3D(i % es.Count, (i % es.Count * es.Count * 3) / es.Count, i / (es.Count * es.Count * 3))).ToArray();

            //var rnd = new Random();
            //this.Values = Data.ToArray();
            int k = 0;
            int Rows, Columns;
            Rows = es.Count;
            Columns = 1;
            double[] V = new double[es.Count];

            var data = new Point3D[es.Count];
             for (int i = 0; i < Rows - 1; i++)
             {
                 for (int j = 0; j < Columns ; j++)
                 {
                     V[k] = es[Convert.ToString(i)].Y / 10000;
                     k++;


                     data[i] = new Point3D(es[Convert.ToString(i)].X, j, es[Convert.ToString(i)].Y);
                         

                 }
             }
             this.Data = data;
            this.Values = V;
           RaisePropertyChanged("Data");
           RaisePropertyChanged("Values");
            RaisePropertyChanged("SurfaceBrush");
        }
        public Point3D[,] CreateDataArray(NMRMBC.diffusion diff)
        {
            NMRMBC.espectrum es;
            es = diff["10"];
            int Rows, Columns;
            Rows = es.Count;
            Columns = 1;
            var data = new Point3D[es.Count, 1];
            for (int i = 0; i < Rows - 1; i++) 
            {
                for (int j = 0; j < Columns; j++)
                {

                    data[i, j] = new Point3D(es[Convert.ToString(i)].X, j, es[Convert.ToString(i)].Y);
                }
            }
               
                    
                
            return data;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string property)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(property));
            }
        }
    }

    //public class SurfacePlotVisual3D : ModelVisual3D
    //{
    //    public static readonly DependencyProperty PointsProperty =
    //        DependencyProperty.Register("Points", typeof(Point3D[,]), typeof(SurfacePlotVisual3D),
    //                                    new UIPropertyMetadata(null, ModelChanged));

    //    public static readonly DependencyProperty ColorValuesProperty =
    //        DependencyProperty.Register("ColorValues", typeof(double[,]), typeof(SurfacePlotVisual3D),
    //                                    new UIPropertyMetadata(null, ModelChanged));

    //    public static readonly DependencyProperty SurfaceBrushProperty =
    //        DependencyProperty.Register("SurfaceBrush", typeof(Brush), typeof(SurfacePlotVisual3D),
    //                                    new UIPropertyMetadata(null, ModelChanged));

    //    private readonly ModelVisual3D visualChild;

    //    public SurfacePlotVisual3D()
    //    {
    //        IntervalX = 0.5;
    //        IntervalY = 0.5;
    //        IntervalZ = 0.5;
    //        FontSize = 0.1;
    //        LineThickness = 0.001;

    //        visualChild = new ModelVisual3D();
    //        Children.Add(visualChild);
    //        //Children.Add(new GridLinesVisual3D());
    //    }

    //    /// <summary>
    //    /// Gets or sets the points defining the surface.
    //    /// </summary>
    //    public Point3D[,] Points
    //    {
    //        get { return (Point3D[,])GetValue(PointsProperty); }
    //        set { SetValue(PointsProperty, value); }
    //    }

    //    /// <summary>
    //    /// Gets or sets the color values corresponding to the Points array.
    //    /// The color values are used as Texture coordinates for the surface.
    //    /// Remember to set the SurfaceBrush, e.g. by using the BrushHelper.CreateGradientBrush method.
    //    /// If this property is not set, the z-value of the Points will be used as color value.
    //    /// </summary>
    //    public double[,] ColorValues
    //    {
    //        get { return (double[,])GetValue(ColorValuesProperty); }
    //        set { SetValue(ColorValuesProperty, value); }
    //    }

    //    /// <summary>
    //    /// Gets or sets the brush used for the surface.
    //    /// </summary>
    //    public Brush SurfaceBrush
    //    {
    //        get { return (Brush)GetValue(SurfaceBrushProperty); }
    //        set { SetValue(SurfaceBrushProperty, value); }
    //    }


    //    // todo: make Dependency properties
    //    public double IntervalX { get; set; }
    //    public double IntervalY { get; set; }
    //    public double IntervalZ { get; set; }
    //    public double FontSize { get; set; }
    //    public double LineThickness { get; set; }

    //    private static void ModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    //    {
    //        ((SurfacePlotVisual3D)d).UpdateModel();
    //    }

    //    private void UpdateModel()
    //    {
    //        visualChild.Content = CreateModel();
    //    }

    //    private Model3D CreateModel()
    //    {
    //        var plotModel = new Model3DGroup();

    //        int rows = Points.GetUpperBound(0) + 1;
    //        int columns = Points.GetUpperBound(1) + 1;
    //        double minX = double.MaxValue;
    //        double maxX = double.MinValue;
    //        double minY = double.MaxValue;
    //        double maxY = double.MinValue;
    //        double minZ = double.MaxValue;
    //        double maxZ = double.MinValue;
    //        double minColorValue = double.MaxValue;
    //        double maxColorValue = double.MinValue;
    //        for (int i = 0; i < rows; i++)
    //            for (int j = 0; j < columns; j++)
    //            {
    //                double x = Points[i, j].X;
    //                double y = Points[i, j].Y;
    //                double z = Points[i, j].Z;
    //                maxX = Math.Max(maxX, x);
    //                maxY = Math.Max(maxY, y);
    //                maxZ = Math.Max(maxZ, z);
    //                minX = Math.Min(minX, x);
    //                minY = Math.Min(minY, y);
    //                minZ = Math.Min(minZ, z);
    //                if (ColorValues != null)
    //                {
    //                    maxColorValue = Math.Max(maxColorValue, ColorValues[i, j]);
    //                    minColorValue = Math.Min(minColorValue, ColorValues[i, j]);
    //                }
    //            }

    //        // make color value 0 at texture coordinate 0.5
    //        if (Math.Abs(minColorValue) < Math.Abs(maxColorValue))
    //            minColorValue = -maxColorValue;
    //        else
    //            maxColorValue = -minColorValue;

    //        // set the texture coordinates by z-value or ColorValue
    //        var texcoords = new Point[rows, columns];
    //        for (int i = 0; i < rows; i++)
    //            for (int j = 0; j < columns; j++)
    //            {
    //                double u = (Points[i, j].Z - minZ) / (maxZ - minZ);
    //                if (ColorValues != null)
    //                    u = (ColorValues[i, j] - minColorValue) / (maxColorValue - minColorValue);
    //                texcoords[i, j] = new Point(u, u);
    //            }

    //        var surfaceMeshBuilder = new MeshBuilder();
    //        surfaceMeshBuilder.AddRectangularMesh(Points, texcoords);

    //        var surfaceModel = new GeometryModel3D(surfaceMeshBuilder.ToMesh(),
    //                                               MaterialHelper.CreateMaterial(SurfaceBrush, null, null, 1, 0));
    //        surfaceModel.BackMaterial = surfaceModel.Material;

    //        var axesMeshBuilder = new MeshBuilder();
    //        for (double x = minX; x <= maxX; x += IntervalX)
    //        {
    //            double j = (x - minX) / (maxX - minX) * (columns - 1);
    //            var path = new List<Point3D> { new Point3D(x, minY, minZ) };
    //            for (int i = 0; i < rows; i++)
    //            {
    //                path.Add(BilinearInterpolation(Points, i, j));
    //            }
    //            path.Add(new Point3D(x, maxY, minZ));

    //            axesMeshBuilder.AddTube(path, LineThickness, 9, false);
    //            GeometryModel3D label = TextCreator.CreateTextLabelModel3D(x.ToString(), Brushes.Black, true, FontSize,
    //                                                                       new Point3D(x, minY - FontSize * 2.5, minZ),
    //                                                                       new Vector3D(1, 0, 0), new Vector3D(0, 1, 0));
    //            plotModel.Children.Add(label);
    //        }

    //        {
    //            GeometryModel3D label = TextCreator.CreateTextLabelModel3D("ppm", Brushes.Black, true, FontSize,
    //                                                                       new Point3D((minX + maxX) * 0.5,
    //                                                                                   minY - FontSize * 6, minZ),
    //                                                                       new Vector3D(1, 0, 0), new Vector3D(0, 1, 0));
    //            plotModel.Children.Add(label);
    //        }

    //        for (double y = minY; y <= maxY; y += IntervalY)
    //        {
    //            double i = (y - minY) / (maxY - minY) * (rows - 1);
    //            var path = new List<Point3D> { new Point3D(minX, y, minZ) };
    //            for (int j = 0; j < columns; j++)
    //            {
    //                path.Add(BilinearInterpolation(Points, i, j));
    //            }
    //            path.Add(new Point3D(maxX, y, minZ));

    //            axesMeshBuilder.AddTube(path, LineThickness, 9, false);
    //            GeometryModel3D label = TextCreator.CreateTextLabelModel3D(y.ToString(), Brushes.Black, true, FontSize,
    //                                                                       new Point3D(minX - FontSize * 3, y, minZ),
    //                                                                       new Vector3D(1, 0, 0), new Vector3D(0, 1, 0));
    //            plotModel.Children.Add(label);
    //        }
    //        {
    //            GeometryModel3D label = TextCreator.CreateTextLabelModel3D("G2", Brushes.Black, true, FontSize,
    //                                                                       new Point3D(minX - FontSize * 10,
    //                                                                                   (minY + maxY) * 0.5, minZ),
    //                                                                       new Vector3D(0, 1, 0), new Vector3D(-1, 0, 0));
    //            plotModel.Children.Add(label);
    //        }
    //        double z0 = (int)(minZ / IntervalZ) * IntervalZ;
    //        for (double z = z0; z <= maxZ + double.Epsilon; z += IntervalZ)
    //        {
    //            GeometryModel3D label = TextCreator.CreateTextLabelModel3D(z.ToString(), Brushes.Black, true, FontSize,
    //                                                                       new Point3D(minX - FontSize * 3, maxY, z),
    //                                                                       new Vector3D(1, 0, 0), new Vector3D(0, 0, 1));
    //            plotModel.Children.Add(label);
    //        }
    //        {
    //            GeometryModel3D label = TextCreator.CreateTextLabelModel3D("Intensity", Brushes.Black, true, FontSize,
    //                                                                       new Point3D(minX - FontSize * 10, maxY,
    //                                                                                   (minZ + maxZ) * 0.5),
    //                                                                       new Vector3D(0, 0, 1), new Vector3D(1, 0, 0));
    //            plotModel.Children.Add(label);
    //        }


    //        var bb = new Rect3D(minX, minY, minZ, maxX - minX, maxY - minY, 0 * (maxZ - minZ));
    //        axesMeshBuilder.AddBoundingBox(bb, LineThickness);

    //        var axesModel = new GeometryModel3D(axesMeshBuilder.ToMesh(), Materials.Black);

    //        plotModel.Children.Add(surfaceModel);
    //        plotModel.Children.Add(axesModel);

    //        return plotModel;
    //    }

    //    private static Point3D BilinearInterpolation(Point3D[,] p, double i, double j)
    //    {
    //        int n = p.GetUpperBound(0);
    //        int m = p.GetUpperBound(1);
    //        var i0 = (int)i;
    //        var j0 = (int)j;
    //        if (i0 + 1 >= n) i0 = n - 2;
    //        if (j0 + 1 >= m) j0 = m - 2;

    //        if (i < 0) i = 0;
    //        if (j < 0) j = 0;
    //        double u = i - i0;
    //        double v = j - j0;
    //        Vector3D v00 = p[i0, j0].ToVector3D();
    //        Vector3D v01 = p[i0, j0 + 1].ToVector3D();
    //        Vector3D v10 = p[i0 + 1, j0].ToVector3D();
    //        Vector3D v11 = p[i0 + 1, j0 + 1].ToVector3D();
    //        Vector3D v0 = v00 * (1 - u) + v10 * u;
    //        Vector3D v1 = v01 * (1 - u) + v11 * u;
    //        return (v0 * (1 - v) + v1 * v).ToPoint3D();
    //    }
    //}
    public class ScatterPlotVisual3D : ModelVisual3D
    {
        public static readonly DependencyProperty PointsProperty =
            DependencyProperty.Register("Points", typeof(Point3D[]), typeof(ScatterPlotVisual3D),
                                        new UIPropertyMetadata(null, ModelChanged));

        public static readonly DependencyProperty ValuesProperty =
            DependencyProperty.Register("Values", typeof(double[]), typeof(ScatterPlotVisual3D),
                                        new UIPropertyMetadata(null, ModelChanged));

        public static readonly DependencyProperty SurfaceBrushProperty =
            DependencyProperty.Register("SurfaceBrush", typeof(Brush), typeof(ScatterPlotVisual3D),
                                        new UIPropertyMetadata(null, ModelChanged));

        private readonly ModelVisual3D visualChild;

        public ScatterPlotVisual3D()
        {
            IntervalX = 1;
            IntervalY = 1;
            IntervalZ = 1;
            FontSize = 0.06;
            SphereSize = 0.09;
            LineThickness = 0.01;

            visualChild = new ModelVisual3D();
            Children.Add(visualChild);
        }

        /// <summary>
        /// Gets or sets the points defining the surface.
        /// </summary>
        public Point3D[] Points
        {
            get { return (Point3D[])GetValue(PointsProperty); }
            set { SetValue(PointsProperty, value); }
        }

        public double[] Values
        {
            get { return (double[])GetValue(ValuesProperty); }
            set { SetValue(ValuesProperty, value); }
        }

        /// <summary>
        /// Gets or sets the brush used for the surface.
        /// </summary>
        public Brush SurfaceBrush
        {
            get { return (Brush)GetValue(SurfaceBrushProperty); }
            set { SetValue(SurfaceBrushProperty, value); }
        }


        // todo: make Dependency properties
        public double IntervalX { get; set; }
        public double IntervalY { get; set; }
        public double IntervalZ { get; set; }
        public double FontSize { get; set; }
        public double SphereSize { get; set; }
        public double LineThickness { get; set; }

        private static void ModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ScatterPlotVisual3D)d).UpdateModel();
        }

        private void UpdateModel()
        {
            visualChild.Content = CreateModel();
        }

        private Model3D CreateModel()
        {
            var plotModel = new Model3DGroup();
            if (Points == null || Values == null) return plotModel;

            double minX = Points.Min(p => p.X);
            double maxX = Points.Max(p => p.X);
            double minY = Points.Min(p => p.Y);
            double maxY = Points.Max(p => p.Y);
            double minZ = Points.Min(p => p.Z);
            double maxZ = Points.Max(p => p.Z);
            double minValue = Values.Min();
            double maxValue = Values.Max();
            //double minValue = minZ;
            //double maxValue = maxZ;
            var valueRange = maxValue - minValue;

            var scatterMeshBuilder = new MeshBuilder(true, true);

            var oldTCCount = 0;
            for (var i = 0; i < Points.Length; ++i)
            {
                scatterMeshBuilder.AddSphere(Points[i], SphereSize, 4, 4);

                var u = (Values[i] - minValue) / valueRange;

                var newTCCount = scatterMeshBuilder.TextureCoordinates.Count;
                for (var j = oldTCCount; j < newTCCount; ++j)
                {
                    scatterMeshBuilder.TextureCoordinates[j] = new Point(u, u);
                }
                oldTCCount = newTCCount;
            }

            var scatterModel = new GeometryModel3D(scatterMeshBuilder.ToMesh(),
                                                   MaterialHelper.CreateMaterial(SurfaceBrush, null, null, 1, 0));
            scatterModel.BackMaterial = scatterModel.Material;

            // create bounding box with axes indications
            var axesMeshBuilder = new MeshBuilder();
            for (double x = minX; x <= maxX; x += IntervalX)
            {
                GeometryModel3D label = TextCreator.CreateTextLabelModel3D(x.ToString(), Brushes.Black, true, FontSize,
                                                                           new Point3D(x, minY - FontSize * 2.5, minZ),
                                                                           new Vector3D(1, 0, 0), new Vector3D(0, 1, 0));
                plotModel.Children.Add(label);
            }

            {
                GeometryModel3D label = TextCreator.CreateTextLabelModel3D("ppm", Brushes.Black, true, FontSize,
                                                                           new Point3D((minX + maxX) * 0.5,
                                                                                       minY - FontSize * 6, minZ),
                                                                           new Vector3D(1, 0, 0), new Vector3D(0, 1, 0));
                plotModel.Children.Add(label);
            }

            for (double y = minY; y <= maxY; y += IntervalY)
            {
                GeometryModel3D label = TextCreator.CreateTextLabelModel3D(y.ToString(), Brushes.Black, true, FontSize,
                                                                           new Point3D(minX - FontSize * 3, y, minZ),
                                                                           new Vector3D(1, 0, 0), new Vector3D(0, 1, 0));
                plotModel.Children.Add(label);
            }
            {
                GeometryModel3D label = TextCreator.CreateTextLabelModel3D("G2", Brushes.Black, true, FontSize,
                                                                           new Point3D(minX - FontSize * 10,
                                                                                       (minY + maxY) * 0.5, minZ),
                                                                           new Vector3D(0, 1, 0), new Vector3D(-1, 0, 0));
                plotModel.Children.Add(label);
            }
            double z0 = (int)(minZ / IntervalZ) * IntervalZ;
            for (double z = z0; z <= maxZ + double.Epsilon; z += IntervalZ)
            {
                GeometryModel3D label = TextCreator.CreateTextLabelModel3D(z.ToString(), Brushes.Black, true, FontSize,
                                                                           new Point3D(minX - FontSize * 3, maxY, z),
                                                                           new Vector3D(1, 0, 0), new Vector3D(0, 0, 1));
                plotModel.Children.Add(label);
            }
            {
                GeometryModel3D label = TextCreator.CreateTextLabelModel3D("Intensity", Brushes.Black, true, FontSize,
                                                                           new Point3D(minX - FontSize * 10, maxY,
                                                                                       (minZ + maxZ) * 0.5),
                                                                           new Vector3D(0, 0, 1), new Vector3D(1, 0, 0));
                plotModel.Children.Add(label);
            }

            var bb = new Rect3D(minX, minY, minZ, maxX - minX, maxY - minY, maxZ - minZ);
            axesMeshBuilder.AddBoundingBox(bb, LineThickness);

            var axesModel = new GeometryModel3D(axesMeshBuilder.ToMesh(), Materials.Black);

            plotModel.Children.Add(scatterModel);
            plotModel.Children.Add(axesModel);

            return plotModel;
        }
    }
}
