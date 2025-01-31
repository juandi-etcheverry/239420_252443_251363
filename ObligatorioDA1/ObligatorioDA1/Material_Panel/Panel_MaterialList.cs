﻿using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BusinessLogic;
using BusinessLogicExceptions;
using Domain;

namespace ObligatorioDA1.Material_Panel
{
    public partial class Panel_MaterialList : UserControl
    {
        private readonly MaterialLogic _materialLogic = new MaterialLogic();
        private readonly Panel_General _panelGeneral;

        public Panel_MaterialList(Panel_General userControl)
        {
            _panelGeneral = userControl;
            InitializeComponent();
            InitializeList();
        }

        public void RefreshMaterialList()
        {
            lblEliminationException.Visible = false;
            dgvMaterialList.Rows.Clear();
            foreach (var material in _materialLogic.GetClientMaterials().ToList())
            {
                if (material is MetallicMaterial)
                {
                    MetallicMaterial metallic = (MetallicMaterial)material;
                    dgvMaterialList.Rows.Add(null, null, null, metallic.MaterialName, "Metallic", metallic.Blur, metallic.ColorX, metallic.ColorY, metallic.ColorZ);
                }
                else
                {
                    dgvMaterialList.Rows.Add(null, null, null, material.MaterialName, "Lambertian", "-", material.ColorX, material.ColorY,material.ColorZ);
                }
            }
        }

        private void InitializeList()
        {
            AddColumns();
            SetDisplayOrderColumns();
            SetWidthColumns();
        }

        private void btnAddMaterial_Click(object sender, EventArgs e)
        {
            _panelGeneral.GoToAddNewMaterial();
        }

        private void dgvMaterialList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                string materialName;
                Material material;
                if (dgvMaterialList.Columns[e.ColumnIndex].Name == "Delete")
                    try
                    {
                        materialName = dgvMaterialList.CurrentRow.Cells[3].Value.ToString();
                        material = _materialLogic.Get(materialName);
                        _materialLogic.Remove(material);
                        dgvMaterialList.Rows.Remove(dgvMaterialList.CurrentRow);
                        lblEliminationException.Visible = false;
                    }
                    catch (AssociationException AssEx)
                    {
                        lblEliminationException.Visible = true;
                        lblEliminationException.Text = AssEx.Message;
                    }

                if (dgvMaterialList.Columns[e.ColumnIndex].Name == "Rename")
                {
                    materialName = dgvMaterialList.CurrentRow.Cells[3].Value.ToString();
                    material = _materialLogic.Get(materialName);
                    _panelGeneral.GoToMaterialRename(material);
                }
            }
        }

        private void dgvMaterialList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                dgvMaterialList.Rows[e.RowIndex].Height = 50;
                if (dgvMaterialList.Columns[e.ColumnIndex].Name == "Colour1")
                {
                    var cell = dgvMaterialList.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    var materialName = dgvMaterialList.Rows[e.RowIndex].Cells[3].Value.ToString();
                    var material = _materialLogic.Get(materialName);
                    var newColor = GetColour(material);
                    PaintCell(cell, newColor);
                }
            }
        }

        private void AddColumns()
        {
            dgvMaterialList.Columns.Add("Colour1", "");
            dgvMaterialList.Columns.Add("Name", "Name");
            dgvMaterialList.Columns.Add("Type", "Type");
            dgvMaterialList.Columns.Add("Blur", "Blur");
            dgvMaterialList.Columns.Add("R", "R");
            dgvMaterialList.Columns.Add("G", "G");
            dgvMaterialList.Columns.Add("B", "B");
        }

        private void SetDisplayOrderColumns()
        {
            dgvMaterialList.Columns["Colour1"].DisplayIndex = 0;
            dgvMaterialList.Columns["Name"].DisplayIndex = 1;
            dgvMaterialList.Columns["Type"].DisplayIndex = 2;
            dgvMaterialList.Columns["Blur"].DisplayIndex = 3;
            dgvMaterialList.Columns["R"].DisplayIndex = 4;
            dgvMaterialList.Columns["G"].DisplayIndex = 5;
            dgvMaterialList.Columns["B"].DisplayIndex = 6;
            dgvMaterialList.Columns["Rename"].DisplayIndex = 7;
            dgvMaterialList.Columns["Delete"].DisplayIndex = 8;
        }

        private void SetWidthColumns()
        {
            dgvMaterialList.Columns["Colour1"].Width = 30;
            dgvMaterialList.Columns["R"].Width = 30;
            dgvMaterialList.Columns["G"].Width = 30;
            dgvMaterialList.Columns["B"].Width = 30;
        }

        private Color GetColour(Material _material)
        {
            var r = _material.ColorX;
            var g = _material.ColorY;
            var b = _material.ColorZ;
            var newColor = Color.FromArgb(r, g, b);
            return newColor;
        }

        private void PaintCell(DataGridViewCell cell, Color color)
        {
            cell.Style.SelectionBackColor = color;
            cell.Style.BackColor = color;
            cell.Style.ForeColor = color;
            cell.ReadOnly = true;
        }
    }
}