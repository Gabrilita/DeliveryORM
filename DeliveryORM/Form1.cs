using DeliveryORM.Controlls;
using DeliveryORM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeliveryORM
{
    public partial class Form1 : Form
    {
        DishController dishesController = new DishController();
        DishTypeController dishTypesController = new DishTypeController();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            List<DishType> allDishTypes = dishTypesController.GetAllDishTypes();
            cmbDishType.DataSource = allDishTypes;
            cmbDishType.DisplayMember = "Name";
            cmbDishType.ValueMember = "Id";
            //btnSelectAll_Click(sender, e);
        }
        private void LoadRecord(Dish dish)
        {
            txtId.Text = dish.Id.ToString();
            txtName.Text = dish.Name;
            txtDescription.Text = dish.Description;
            txtPrice.Text = dish.Price.ToString();
            txtWeight.Text = dish.Weight.ToString();
            cmbDishType.Text = dish.DishTypes.Name;
        }
        private void ClearScreen()
        {
            txtId.Clear();
            txtName.Clear();
            txtPrice.Clear();
            txtWeight.Clear();
            txtDescription.Clear();
            cmbDishType.Text = "";
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || txtName.Text == "")
            {
                MessageBox.Show("Въведете данни!");
                txtName.Focus();
                return;
            }
            Dish newDish = new Dish();
            newDish.Name = txtName.Text;
            newDish.Description = txtDescription.Text;
            newDish.Price = decimal.Parse(txtPrice.Text);
            newDish.Weight = int.Parse(txtWeight.Text);
            newDish.DishTypeId = (int)cmbDishType.SelectedValue;
            dishesController.Create(newDish);
            MessageBox.Show("Записът е успешно добавен!");
            ClearScreen();
            btnSelectAll_Click(sender, e);
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int findId = 0;
            if (string.IsNullOrEmpty(txtId.Text) || !txtId.Text.All(char.IsDigit))
            {
                MessageBox.Show("Въведете Id за търсене!");
                txtId.Focus();
                return;
            }
            findId = int.Parse(txtId.Text);
            if (string.IsNullOrEmpty(txtName.Text))
            {
                Dish findedDish = dishesController.Get(findId);
                if (findedDish == null)
                {
                    MessageBox.Show("НЯМА ТАКЪВ ЗАПИС в БД! \n Въведете Id за търсене!");
                    txtId.Focus();
                    return;
                }
                LoadRecord(findedDish);
            }
            else
            {
                Dish updatedDish = new Dish();
                updatedDish.Name = txtName.Text;
                updatedDish.Description = txtDescription.Text;
                updatedDish.Price = decimal.Parse(txtPrice.Text);
                updatedDish.Weight = int.Parse(txtWeight.Text);
                updatedDish.DishTypeId = (int)cmbDishType.SelectedValue;

                dishesController.Update(findId, updatedDish);
            }
            MessageBox.Show("Успешен update!");
            btnSelectAll_Click(sender, e);
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int findId = 0;
            if (string.IsNullOrEmpty(txtId.Text) || !txtId.Text.All(char.IsDigit))
            {
                MessageBox.Show("Въведете Id за търсене!");
                txtId.Focus();
                return;
            }
            else
            {
                findId = int.Parse(txtId.Text);
            }
            Dish findedDish = dishesController.Get(findId);
            if (findedDish == null)
            {
                MessageBox.Show("НЯМА ТАКЪВ ЗАПИС в БД! \nВъведете Id за търсене!");
                txtId.Focus();
                return;
            }
            LoadRecord(findedDish);
            DialogResult answer = MessageBox.Show("Наистина ли искате да изтриете запис No " + findId + "?",
                "PROMPT",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                dishesController.Delete(findId);
            }
            btnSelectAll_Click(sender, e);
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            int findId = 0;
            if (string.IsNullOrEmpty(txtId.Text) || !txtId.Text.All(char.IsDigit))
            {
                MessageBox.Show("Въведете Id за търсене!");
                txtId.Focus();
                return;
            }
            else
            {
                findId = int.Parse(txtId.Text);
            }
            if (string.IsNullOrEmpty(txtName.Text))
            {
                Dish findedDish = dishesController.Get(findId);
                if (findedDish == null)
                {
                    MessageBox.Show("НЯМА ТАКЪВ ЗАПИС в БД! \nВъведете Id за търсене!");
                    txtId.Focus();
                    return;
                }
                LoadRecord(findedDish);
            }
        }
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            List<Dish> allDishes = dishesController.GetAll();
            listBox1.Items.Clear();
            foreach (var item in allDishes)
            {
                listBox1.Items.Add($"{item.Id}, {item.Name}, Цена: {item.Price} лв., Грамаж: {item.Weight} гр., Тип ястие: {item.DishTypes.Name}, Описание: {item.Description}");
               // listBox1.Items.Add($"{item.Id}, {item.Name}");
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearScreen();
        }
    }
}

