using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.TextField;
using Xamarin.Forms;



namespace income_Planning
{
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.AppCompat.Light.NoActionBar", Icon = "@mipmap/icon" ,MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText incomePerHourEditText;
        EditText workHoursPerDayEditText;
        EditText taxRateEditText;
        EditText savingsRateEditText;
        
        TextView workSummaryTextView;
        TextView grossIncomeTextView;
        TextView taxPayableTextView;
        TextView annualSavingsTextView;
        TextView spendableIncomeTextView;

        Android.Widget.Button calculateButton;
        Android.Widget.RelativeLayout relativeLayout;
        
        bool inputCalculated = false;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            ConnectViews();
        }

        void ConnectViews()
        {

            incomePerHourEditText = FindViewById<EditText>(Resource.Id.incomePerHourEditText);
            workHoursPerDayEditText = (EditText)FindViewById(Resource.Id.workHoursPerDayEditText);
            taxRateEditText = FindViewById<EditText>(Resource.Id.taxRateEditText);
            savingsRateEditText = FindViewById<EditText>(Resource.Id.savingsRateEditText);

            workSummaryTextView = FindViewById<TextView>(Resource.Id.workSummaryTextView);
            grossIncomeTextView = FindViewById<TextView>(Resource.Id.grossIncomeTextView);
            taxPayableTextView = FindViewById<TextView>(Resource.Id.taxPayableTextView);
            annualSavingsTextView = FindViewById<TextView>(Resource.Id.annualSavingsTextView);
            spendableIncomeTextView = FindViewById<TextView>(Resource.Id.spendableIncomeTextView);

            calculateButton = FindViewById<Android.Widget.Button>(Resource.Id.calculateButton);
            relativeLayout = (Android.Widget.RelativeLayout)FindViewById(Resource.Id.relativeLayout);

            calculateButton.Click += CalculateButton_Click;

        }

        private void CalculateButton_Click(object sender, System.EventArgs e)
        {

            if (inputCalculated) { 
                inputCalculated = false;
                calculateButton.Text = "Calculate";
                ClearInput();
                return;
            }

            //throw new System.NotImplementedException();
            double incomePerHour = double.Parse(incomePerHourEditText.Text);
            double workHoursPerDay = double.Parse(workHoursPerDayEditText.Text);
            double taxRate = double.Parse(taxRateEditText.Text);   
            double savingsRate = double.Parse(savingsRateEditText.Text);

            double annualWorkHourSummary = workHoursPerDay * 5 * 50;
            double annualincome = incomePerHour * workHoursPerDay * 5 * 50;
            double taxpayable = (taxRate / 100) * annualincome;
            double annualSavings = (savingsRate / 100) * annualincome;
            double spendableIncome = annualincome - annualSavings - taxpayable;

             workSummaryTextView.Text = annualWorkHourSummary.ToString("#,##") + " HRS";
             grossIncomeTextView.Text = annualincome.ToString("#,##") + " USD";
             taxPayableTextView.Text = taxpayable.ToString("#,##") + " USD";
             annualSavingsTextView.Text = annualSavings.ToString("#,##") + " USD";
             spendableIncomeTextView.Text = spendableIncome.ToString("#,##") + " USD";

            relativeLayout.Visibility = Android.Views.ViewStates.Visible;
            inputCalculated = true;
            calculateButton.Text = "Clear";

        }

        void ClearInput() {

             incomePerHourEditText.Text = "";
             workHoursPerDayEditText.Text = "";
             taxRateEditText.Text = "";
             savingsRateEditText.Text = "";

            relativeLayout.Visibility = ViewStates.Invisible;

        }
    }
}