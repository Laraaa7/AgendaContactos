﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Contactoss
{
    public partial class Form1 : Form
    {
        private List<Contact> contacts;
        public Form1()
        {
            InitializeComponent();
            contacts = new List<Contact>();
            btnInsertar.Click += btnInsertar_Click;
            btnBuscar.Click += btnBuscar_Click;
            btnActualizar.Click += btnActualizar_Click;
            btnEliminar.Click += btnEliminar_Click;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private bool checkPhone(string phone)
        {
            if (phone == null || phone.Equals(""))
            {
                MessageBox.Show("ERROR: el número de teléfono no puede estar vacío.");
                return false;
            }
            if (phone.Length != 11)
            {
                MessageBox.Show("ERROR: el número de teléfono debe tener 11 dígitos.");
                return false;
            }
            for (int i = 0; i < phone.Length; i++)
            {
                if (phone[i] < '0' || phone[i] > '9')
                {
                    MessageBox.Show("ERROR: el número de teléfono debe tener solo números.");
                    return false;
                }
            }
            return true;
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            string name = txtNombre.Text;
            string phone = txtTelefono.Text;

            if (string.IsNullOrEmpty(name) || !checkPhone(phone))
            {
                return;
            }

            Contact contact = new Contact(name, phone);
            contacts.Add(contact);

            ListViewItem item = new ListViewItem(contact.Name);
            item.SubItems.Add(contact.Phone);
            listViewContactos.Items.Add(item);

            MessageBox.Show("Contacto añadido correctamente");
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string phone = txtTelefono.Text;

            if (string.IsNullOrEmpty(phone) || !checkPhone(phone))
            {
                MessageBox.Show("Introduce un número de teléfono válido antes de buscar");
                return;
            }

            var contact = contacts.Find(c => c.Phone == phone);

            if (contact != null)
            {
                txtNombre.Text = contact.Name;
                txtTelefono.Text = contact.Phone;
                MessageBox.Show("Se encontró el siguiente contacto:\nNombre: " + contact.Name + "\nTeléfono: " + contact.Phone);
            }
            else
            {
                MessageBox.Show("No se encontró ningún contacto con el número de teléfono " + phone);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string phone = txtTelefono.Text;
            if (!checkPhone(phone))
            {
                return;
            }

            var contact = contacts.Find(c => c.Phone == phone);
            if (contact != null)
            {
                contacts.Remove(contact);
                foreach (ListViewItem item in listViewContactos.Items)
                {
                    if (item.SubItems[1].Text == phone)
                    {
                        listViewContactos.Items.Remove(item);
                        break;
                    }
                }
                MessageBox.Show("Contacto eliminado correctamente");
            }
            else
            {
                MessageBox.Show("No se encontró ningún contacto con esos datos");
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string phone = txtTelefono.Text;
            string newName = txtNombre.Text;

            if (string.IsNullOrEmpty(phone) || !checkPhone(phone))
            {
                MessageBox.Show("Error, introduce un número de teléfono válido");
                return;
            }

            int index = contacts.FindIndex(c => c.Phone == phone);

            if (index == -1)
            {
                MessageBox.Show("No se encontró ningún contacto");
            }
            else
            {
                contacts[index].Name = newName;

                foreach (ListViewItem item in listViewContactos.Items)
                {
                    if (item.SubItems[1].Text == phone)
                    {
                        item.Text = newName;
                        break;
                    }
                }
                MessageBox.Show("Contacto actualizado correctamente:\nNombre: " + newName + "\nTeléfono: " + phone);
            }
        }


        private void listViewContactos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
