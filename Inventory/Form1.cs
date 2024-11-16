using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.ComponentModel;

namespace Inventory
{
    public partial class frmAddProduct : Form
    {
        private string _ProductName, _Category, _MfgDate, _ExpDate, _Description;
        private int _Quantity;
        private double _SellPrice;

        private string[] ListOfProductCategory = {
            "Beverages", "Bread/Bakery", "Canned/Jarred Goods", "Dairy",
            "Frozen Goods", "Meat", "Personal Care", "Other"
        };

        private BindingSource showProductList = new BindingSource();

        public frmAddProduct()
        {
            InitializeComponent();
            showProductList.DataSource = typeof(ProductClass); 
            GridViewProductList.DataSource = showProductList;
        }

        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            foreach (string category in ListOfProductCategory)
            {
                cbCategory.Items.Add(category);
            }
        }

        public string Product_Name(string name)
        {
            if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                throw new Exception("Product name must contain only letters.");
            return name;
        }

        public int Quantity(string qty)
        {
            if (!Regex.IsMatch(qty, @"^[0-9]+$"))
                throw new Exception("Quantity must be a positive integer.");
            return Convert.ToInt32(qty);
        }

        public double SellingPrice(string price)
        {
            if (!Regex.IsMatch(price, @"^(\d*\.)?\d+$"))
                throw new Exception("Selling price must be a valid decimal number.");
            return Convert.ToDouble(price);
        }

        public class NumberFormatException : Exception
        {
            public NumberFormatException(string message) : base(message) { }
        }

        public class StringFormatException : Exception
        {
            public StringFormatException(string message) : base(message) { }
        }

        public class CurrencyFormatException : Exception
        {
            public CurrencyFormatException(string message) : base(message) { }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                _ProductName = Product_Name(txtProductName.Text);
                _Category = cbCategory.Text;
                _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
                _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
                _Description = richTxtDescription.Text;

                _Quantity = Quantity(txtQuantity.Text);
                _SellPrice = SellingPrice(txtSellPrice.Text);

                
                showProductList.Add(new ProductClass(
                    _ProductName,
                    _Category,
                    _MfgDate,
                    _ExpDate,
                    _SellPrice,
                    _Quantity,
                    _Description
                ));

                GridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public class ProductClass
    {
        private int _Quantity;
        private double _SellingPrice;
        private string _ProductName, _Category, _ManufacturingDate, _ExpirationDate, _Description;

        public ProductClass(string ProductName, string Category, string MfgDate, string ExpDate, double Price, int Quantity, string Description)
        {
            this._Quantity = Quantity;
            this._SellingPrice = Price;
            this._ProductName = ProductName;
            this._Category = Category;
            this._ManufacturingDate = MfgDate;
            this._ExpirationDate = ExpDate;
            this._Description = Description;
        }

        public string ProductName
        {
            get { return this._ProductName; }
            set { this._ProductName = value; }
        }
        public string Category
        {
            get { return this._Category; }
            set { this._Category = value; }
        }
        public string ManufacturingDate
        {
            get { return this._ManufacturingDate; }
            set { this._ManufacturingDate = value; }
        }
        public string ExpirationDate
        {
            get { return this._ExpirationDate; }
            set { this._ExpirationDate = value; }
        }
        public string Description
        {
            get { return this._Description; }
            set { this._Description = value; }
        }
        public int Quantity
        {
            get { return this._Quantity; }
            set { this._Quantity = value; }
        }
        public double SellingPrice
        {
            get { return this._SellingPrice; }
            set { this._SellingPrice = value; }
        }
    }
}


