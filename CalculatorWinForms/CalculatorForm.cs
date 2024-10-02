using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logic.Domain;

namespace CalculatorWinForms
{
    public partial class CalculatorForm : Form
    {
		private const string errorInvalidInput = "Invalid input";
		private readonly Calculator calculator = new Calculator();
		
		private double _a;
		private double _b;
		private double _temp;
		private string _action;
		
		private bool _needResetAll;
		private bool _needResetNumber;
		private bool _needResetOperands;

		public CalculatorForm()
        {
            InitializeComponent();
            SetControlsDefaultValue();
		}

		private void SetControlsDefaultValue()
		{
            lblNumber.Text = "0";
			lblMemory.Visible = false;
			lblMemory.Text = "M";
			lblOperands.Text = "";

			_needResetAll = false;
			_needResetNumber = false;
			_needResetOperands = false;

			btnMC.Enabled = false;
            btnMR.Enabled = false;
		}

		private void btnMC_Click(object sender, EventArgs e)
		{
			calculator.ClearMemory();
			ClearMemoryButtons();
		}

		private void btnMPlus_Click(object sender, EventArgs e)
		{
			var number = Convert.ToDouble(lblNumber.Text);
			calculator.MAddition(number);
			ActivateMemoryButtons();
		}

		private void btnMMinus_Click(object sender, EventArgs e)
		{
			try
			{
				_temp = Convert.ToDouble(lblNumber.Text);
			}
			catch (Exception)
			{
				ShowError(errorInvalidInput);
			}
			
			calculator.MSubtraction(_temp);
			ActivateMemoryButtons();
		}

		private void ClearMemoryButtons()
		{
			btnMC.Enabled = false;
			btnMR.Enabled = false;
			lblMemory.Visible = false;
		}

		private void ActivateMemoryButtons()
		{
			btnMC.Enabled = true;
			btnMR.Enabled = true;
			lblMemory.Visible = true;
			_needResetNumber = true;
		}

		private void EventKeyPress(object sender, EventArgs e)
		{

		}

		private void btnAdditional_Click(object sender, EventArgs e)
		{
			CalculateResult(false);
			EnterOperator("+");
		}

		private void btnSub_Click(object sender, EventArgs e)
		{
			CalculateResult(false);
			EnterOperator("-");
		}

		private void btnDiv_Click(object sender, EventArgs e)
		{
			CalculateResult(false);
			EnterOperator("/");
		}

		private void btnMultiplication_Click(object sender, EventArgs e)
		{
			CalculateResult(false);
			EnterOperator("*");
		}

		private void btnMR_Click(object sender, EventArgs e)
		{
			try
			{
				lblNumber.Text = calculator.GetMemory().ToString();
			}
			catch(Exception exc)
			{
				ShowError(exc.Message);
			}

			_needResetNumber = true;

		}

		private void btnDel_Click(object sender, EventArgs e)
		{
			var text = lblNumber.Text;
			lblNumber.Text = lblNumber.Text.Remove(text.Length-1);
		}

		private void buttonCE_Click(object sender, EventArgs e)
		{
			lblNumber.Text = "0";
		}

		private void buttonC_Click(object sender, EventArgs e)
		{
			ResetAllFields();
		}

		private void btnChangeSign_Click(object sender, EventArgs e)
		{
			if (lblNumber.Text == "0")
			{
				return;
			}

			if (lblNumber.Text[0] == '-')
			{
				lblNumber.Text = lblNumber.Text.Remove(0, 1);
			}
			else
			{
				lblNumber.Text = "-" + lblNumber.Text;
			}
		}

		private void btn0_Click(object sender, EventArgs e)
		{
			EnterNumber("0");
		}

		private void btn1_Click(object sender, EventArgs e)
		{
			EnterNumber("1");
		}

		private void btn2_Click(object sender, EventArgs e)
		{
			EnterNumber("2");
		}

		private void btn3_Click(object sender, EventArgs e)
		{
			EnterNumber("3");
		}

		private void btn4_Click(object sender, EventArgs e)
		{
			EnterNumber("4");
		}

		private void btn5_Click(object sender, EventArgs e)
		{
			EnterNumber("5");
		}

		private void btn6_Click(object sender, EventArgs e)
		{
			EnterNumber("6");
		}

		private void btn7_Click(object sender, EventArgs e)
		{
			EnterNumber("7");
		}

		private void btn8_Click(object sender, EventArgs e)
		{
			EnterNumber("8");
		}

		private void btn9_Click(object sender, EventArgs e)
		{
			EnterNumber("9");
		}

		private void btnDot_Click(object sender, EventArgs e)
		{
			EnterNumber(",");
		}

		private void btnPercent_Click(object sender, EventArgs e)
		{
			if (_a == 0)
			{
				lblNumber.Text = "0";
			}

			try
			{
				var _b2 = Convert.ToDouble(lblNumber.Text);

				if (String.IsNullOrEmpty(_action))
				{
					_a = _b2;
				}

				lblNumber.Text = calculator.Percent(_a, _b2).ToString();
			}
			catch
			{
				ShowError(errorInvalidInput);
				return;
			}
		}

		private void btnSqrt_Click(object sender, EventArgs e)
		{
			EnterSingleAction("sqrt");
		}

		private void btnSqr_Click(object sender, EventArgs e)
		{
			EnterSingleAction("sqr");
		}

		private void btn1DivX_Click(object sender, EventArgs e)
		{
			EnterSingleAction("1/x");
		}

		private void EnterOperator(string operatorStr)
		{
			try
			{
				_a = Convert.ToDouble(lblNumber.Text);
			}
			catch
			{
				ShowError(errorInvalidInput);
				return;
			}

			if (_needResetOperands)
			{
				ResetOperands();
			}
			
			if (String.IsNullOrEmpty(_action))
			{
				_needResetAll = false;
			}

			_action = operatorStr;
			lblOperands.Text = $"{CorrectingText(lblNumber.Text, true)} {operatorStr}";
			lblNumber.Text = "0";
		}

		private string CorrectingText(string text, bool isNumberEnteredFull = false) 
		{
			if (isNumberEnteredFull)
			{
				if (text[text.Length - 1] == ',')
				{
					 text = text.Remove(text.Length - 1);
				}

				return text;
			}

			if (text[0] == '0'
				&& (text.IndexOf(",") != 1))
			{
				text = text.Remove(0, 1);
			}

			if (text[0] == '-')
			{
				if (text[1] == '0' && (text.IndexOf(",") != 2))
				{
					text = text.Remove(1, 1);
				}
			}

			return text;
		}

		private void EnterSingleAction(string singleAction)
		{
			try
			{
				_b = Convert.ToDouble(lblNumber.Text);
			}
			catch
			{
				ShowError(errorInvalidInput);
				return;
			}

			if (_needResetOperands)
			{
				ResetOperands();
			}

			double result;
			string actionInStr;

			try
			{
				switch (singleAction)
				{
					case "sqrt":
						actionInStr = "sqrt";
						result = calculator.Sqrt(_b);
						break;
					case "sqr":
						actionInStr = "sqr";
						result = calculator.Degree2(_b);
						break;
					case "1/x":
						actionInStr = "1/";
						result = calculator.OneDivX(_b);
						break;
					default:
						throw new Exception("Action not found");
				}
			}
			catch(Exception exc)
			{
				ShowError(errorInvalidInput);
				return;
			}

			lblNumber.Text = result.ToString();
			_needResetNumber = true;

			if (String.IsNullOrEmpty(_action))
			{
				lblOperands.Text = $"{actionInStr}({_b})";
				_needResetOperands = true;
			}
			else
			{
				lblOperands.Text += $" {actionInStr}({_b})";
			}
		}

		private void ShowError(string message)
		{
			ResetAllFields();
			lblNumber.Text = message;
			_needResetAll = true;
		}

		private void EnterNumber(string number)
		{
			if (_needResetAll)
			{
				ResetAllFields();
			}
			else
			{
				if (_needResetNumber)
				{
					ResetNumber();
				}

				if (_needResetOperands)
				{
					ResetOperands();
				}
			}

			var text = lblNumber.Text;

			if (text.Length > 11)
			{
				return;
			}

			text = CorrectingText(lblNumber.Text + number);
			lblNumber.Text = text;
		}

		private void btnResult_Click(object sender, EventArgs e)
		{
			CalculateResult(true);
		}

		private void ResetAllFields()
		{
			_a = 0;
			_b = 0;
			_action = "";

			ResetNumber();
			ResetOperands();

			_needResetAll = false;
		}

		private void ResetNumber()
		{
			lblNumber.Text = "0";
			_needResetNumber = false;
		}

		private void ResetOperands()
		{
			lblOperands.Text = "";
			_needResetOperands = false;
		}

		private void CalculateResult(bool needReset)
		{
			if (String.IsNullOrEmpty(_action))
			{
				return;
			}

			lblOperands.Text += $" {CorrectingText(lblNumber.Text, true)}";

			try
			{
				_b = Convert.ToDouble(lblNumber.Text);
			}
			catch
			{
				ShowError(errorInvalidInput);
				return;
			}

			double resultAction;

			switch (_action)
			{
				case "+":
					resultAction = calculator.Addition(_a, _b);
					break;
				case "-":
					resultAction = calculator.Subtraction(_a, _b);
					break;
				case "*":
					resultAction = calculator.Multiplication(_a, _b);
					break;
				case "/":
					resultAction = calculator.Division(_a, _b);
					break;
				default:
					ShowError("Invalid action");
					return;
			}
			
			_action = "";
			lblNumber.Text = resultAction.ToString();
			_needResetAll = needReset;
		}

	}
}