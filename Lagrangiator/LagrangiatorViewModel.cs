using OxyPlot;
using System;
using System.Collections.Generic;
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
        private string terminalText = "Prepare to get interpolated losers...";

        private bool fileInputVisible = false;

        private readonly PlotModel plotModel;

        private readonly LagrangiatorModel model;

        public LagrangiatorViewModel() : base() {
            this.model = new LagrangiatorModel();
            this.plotModel = new PlotModel();
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
        public int FormulaIndex { get => this.formulaIndex; set=>this.formulaIndex = value; }
        public int InterpolationNodesNumber { get => this.interpolationNodesNumber; set => this.interpolationNodesNumber = value; }
        public double LeftBound { get => this.leftBound; set => this.leftBound = value; }
        public double RightBound { get => this.rightBound; set => this.rightBound = value; }
        public bool ShowNodes { get => this.showNodes; set => this.showNodes = value; }
        public bool ShowInterpolation { get => this.showInterpolation; set => this.showInterpolation = value; }
        public bool ShowFx { 
            get => this.showFx;
            set
            {
                this.showFx = value;
                this.showFxMemory = value;
            }
        }
        public string TerminalText { get => this.terminalText; }


        public bool FileInputVisible { get => this.fileInputVisible; }
        public bool FunctionInputVisible { get => !this.fileInputVisible; }

        public Visibility FileInputVisibility { get => this.FileInputVisible ? Visibility.Visible : Visibility.Hidden; }

        public Visibility FunctionInputVisibility { get => this.FunctionInputVisible ? Visibility.Visible : Visibility.Hidden; }

        public PlotModel PlotModel { get=>this.plotModel; }

    }
}
