using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lagrangiator
{
    class LagrangiatorViewModel : BaseViewModel
    {
        private int dataInputIndex = 0;
        private int formulaIndex = 0;
        private int interpolationNodesNumber = 11;
        private double leftBound = -5;
        private double rightBound = 5;
        private bool showNodes = true;
        private bool showInterpolation = true;
        private bool showFx = false;
        private bool showFxMemory = false;

        private string? filePath = null;

        private ObservableCollection<string> terminalLines = new ObservableCollection<string>();

        private string terminalText = "";

        private bool fileInputVisible = false;


        Command selectFileCommand = new Command();
        Command inputMethodChangeCommand = new Command();


        private readonly PlotModel plotModel;

        private readonly LagrangiatorModel model;

        public LagrangiatorViewModel() : base() {
            this.model = new LagrangiatorModel();
            this.plotModel = new PlotModel();
            this.InitPlot();

            this.selectFileCommand.ExecuteReceived += SelectFileCommand_ExecuteReceived;
            this.inputMethodChangeCommand.ExecuteReceived += InputMethodChangeCommand_ExecuteReceived;

            this.terminalLines.CollectionChanged += TerminalLines_CollectionChanged;
            this.terminalLines.Add("! Lagrangiator 2023");
            this.terminalLines.Add("! Prepare to get interpolated, losers...");

            this.Plot();

        }

        private void InputMethodChangeCommand_ExecuteReceived(object? sender, EventArgs e)
        {
            if(this.DataInputIndex == 0)
            {
                this.Plot();
            }
            else
            {
                this.plotModel.Series.Clear();
                this.plotModel.InvalidatePlot(true);
                OnPropertyChanged(nameof(this.PlotModel));
            }
        }

        private void SelectFileCommand_ExecuteReceived(object? sender, EventArgs e)
        {
            this.filePath = LagrangiatorViewModel.WindowService.ShowFileDialog();
            OnPropertyChanged(nameof(this.FilePath));
            if(this.filePath != null)
            {
                this.Plot();
            }
            
        }

        private void TerminalLines_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (terminalLines.Count > 20)
            {
                terminalLines.RemoveAt(0);
            }
            this.terminalText = String.Join('\n', this.terminalLines);
            OnPropertyChanged(nameof(TerminalText));
        }

        private void InitPlot()
        {
            this.plotModel.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot
            });
            this.plotModel.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot
            });
        }

        private void Plot()
        {
            this.plotModel.Series.Clear();

            Func<double, double> interpolationFunction;
            if (this.dataInputIndex == 0)
            {
                this.model.LeftInterpolationBound = this.LeftBound;
                this.model.RightInterpolationBound = this.RightBound;
                this.model.NumberOfNodes = this.interpolationNodesNumber;
                interpolationFunction = this.model.InterpolateUsingFunctionIndex(this.formulaIndex);

            }
            else
            {
                if (this.filePath == null) throw new NullReferenceException();

                try
                {
                    interpolationFunction = this.model.InterpolateUsingFile((string)this.filePath);
                }
                catch (Exception) { 
                    LagrangiatorViewModel.WindowService.ShowMessage("Invalid file format detected."); 
                    this.plotModel.InvalidatePlot(true); 
                    return; 
                }

                this.leftBound = this.model.LeftInterpolationBound;
                this.rightBound = this.model.RightInterpolationBound;
                this.interpolationNodesNumber = this.model.NumberOfNodes;
                OnPropertyChanged(nameof(this.LeftBound));
                OnPropertyChanged(nameof(this.RightBound));
                OnPropertyChanged(nameof(this.InterpolationNodesNumber));

            }
            
            
            this.plotModel.Series.Add(new FunctionSeries((x) => 0, LeftBound, RightBound, 0.01));

            if (this.showInterpolation)
            {
                this.plotModel.Series.Add(new FunctionSeries(interpolationFunction, LeftBound, RightBound, 0.01, "interpolation"));
            }
            if (this.showNodes)
            {
                ScatterSeries series = new ScatterSeries
                {
                    MarkerFill = OxyColors.Red,
                    MarkerType = MarkerType.Triangle,
                    MarkerSize = 10
                };
                (double, double)[] nodes = this.model.GetNodes(interpolationFunction);
                for(int i=0; i<nodes.Length; i++)
                {
                    series.Points.Add(new ScatterPoint(nodes[i].Item1, nodes[i].Item2));
                }
                this.plotModel.Series.Add(series);
            }
            if (this.showFx)
            {
                this.plotModel.Series.Add(new FunctionSeries(this.model.Function, LeftBound, RightBound, 0.01, "f(x)"));
            }

            this.plotModel.InvalidatePlot(true);
            OnPropertyChanged(nameof(this.PlotModel));

            /*
                double y = model.functions[formulaIndex].Invoke(x);
                ScatterSeries series = new ScatterSeries
                {
                    MarkerFill = MethodIndex == 0 ? OxyColors.Blue : OxyColors.Red,
                    MarkerType = MarkerType.Triangle,
                    MarkerSize = 10
                };
                series.Points.Add(new ScatterPoint(x, y));
                Debug.WriteLine($"x: {x}; y: {y}");

                this.PlotModel.Series.Add(series);
                terminalLines.Add($"Solution marker at ({x}, {y})");
                this.PlotModel.InvalidatePlot(true);
             */
        }

        public int DataInputIndex { 
            get => this.dataInputIndex; 
            set { 
                this.dataInputIndex = value;
                this.fileInputVisible = value == 1;
                OnPropertyChanged(nameof(this.FileInputVisible));
                OnPropertyChanged(nameof(this.FileInputVisibility));
                OnPropertyChanged(nameof(this.FunctionInputVisible));
                OnPropertyChanged(nameof(this.FunctionInputVisibility));
                if(value == 0)
                {
                    this.showFx = this.showFxMemory;
                }
                else
                {
                    this.showFxMemory = this.showFx;
                    this.showFx = false;
                }
                
                OnPropertyChanged(nameof(this.ShowFx));
            }
        }
        public int FormulaIndex { 
            get => this.formulaIndex;
            set
            {
                this.formulaIndex = value;
                this.Plot();
            }
        }
        public int InterpolationNodesNumber { get => this.interpolationNodesNumber; set => this.interpolationNodesNumber = value; }
        public double LeftBound { 
            get => this.leftBound; 
            set {
                this.leftBound = value;
                this.Plot();
            }
        }
        public double RightBound { 
            get => this.rightBound;
            set
            {
                this.rightBound = value;
                this.Plot();
            }
        }
        public bool ShowNodes { 
            get => this.showNodes;
            set { 
                this.showNodes = value;
                this.Plot();
            }
        }
        public bool ShowInterpolation { 
            get => this.showInterpolation; 
            set {
                this.showInterpolation = value;
                this.Plot();
            }
        }
        public bool ShowFx { 
            get => this.showFx;
            set
            {
                this.showFx = value;
                this.showFxMemory = value;
                this.Plot();
            }
        }
        public string TerminalText { get => this.terminalText; }


        public bool FileInputVisible { get => this.fileInputVisible; }
        public bool FunctionInputVisible { get => !this.fileInputVisible; }

        public Visibility FileInputVisibility { get => this.FileInputVisible ? Visibility.Visible : Visibility.Hidden; }

        public Visibility FunctionInputVisibility { get => this.FunctionInputVisible ? Visibility.Visible : Visibility.Hidden; }


        public string FilePath
        {
            get => this.filePath ?? "<No file chosen>";
            set => this.filePath = value;
        }



        public Command SelectFileCommand { get => this.selectFileCommand; }
        public Command InputMethodChangeCommand { get => this.inputMethodChangeCommand; }


        public PlotModel PlotModel { get=>this.plotModel; }

    }
}
