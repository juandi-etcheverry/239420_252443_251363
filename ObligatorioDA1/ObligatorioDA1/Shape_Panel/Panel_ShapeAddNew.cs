﻿using System;
using System.Windows.Forms;
using BusinessLogic;
using BusinessLogicExceptions;
using Domain;

namespace ObligatorioDA1
{
    public partial class Panel_ShapeAddNew : UserControl
    {
        private readonly Panel_General _panelGeneral;
        private readonly ShapeLogic _shapeLogic = new ShapeLogic();
        private Sphere newSphere;

        public Panel_ShapeAddNew(Panel_General userControl)
        {
            _panelGeneral = userControl;
            InitializeComponent();
        }

        public void RefreshShapeAddNew()
        {
            newSphere = new Sphere();
            RefreshPanel();
        }

        private void RefreshPanel()
        {
            txbNewShapeName.Clear();
            txbNewShapeRadius.Clear();
            lblNewShapeNameException.Visible = false;
            lblNewShapeRadiusException.Visible = false;
        }

        private void btnShowAllShapes_Click(object sender, EventArgs e)
        {
            _panelGeneral.GoToShapeList();
        }

        private void btnNewShape_Click(object sender, EventArgs e)
        {
            lblNewShapeNameException.Visible = false;
            lblNewShapeRadiusException.Visible = false;
            try
            {
                double radius;
                var validRadius = double.TryParse(txbNewShapeRadius.Text, out radius);
                if (!validRadius) throw new ArgumentException("Radius must be a decimal number");
                newSphere.ShapeName = txbNewShapeName.Text;
                newSphere.Radius = radius;
                _shapeLogic.AddShape(newSphere);
                _panelGeneral.GoToShapeList();
            }
            catch (NameException nameEx)
            {
                lblNewShapeNameException.Visible = true;
                lblNewShapeNameException.Text = nameEx.Message;
            }
            catch (ArgumentException radEx)
            {
                lblNewShapeRadiusException.Visible = true;
                lblNewShapeRadiusException.Text = radEx.Message;
            }
            catch (NonPositiveRadiusException negRadEx)
            {
                lblNewShapeRadiusException.Visible = true;
                lblNewShapeRadiusException.Text = negRadEx.Message;
            }
        }

        private void txbNewShapeName_TextChanged(object sender, EventArgs e)
        {
            lblNewShapeNameException.Visible = false;
            try
            {
                newSphere.ShapeName = txbNewShapeName.Text;
            }
            catch (NameException nameEx)
            {
                lblNewShapeNameException.Visible = true;
                lblNewShapeNameException.Text = nameEx.Message;
            }
        }

        private void txbNewShapeRadius_TextChanged(object sender, EventArgs e)
        {
            lblNewShapeRadiusException.Visible = false;
            try
            {
                double radius;
                var validRadius = double.TryParse(txbNewShapeRadius.Text, out radius);
                if (!validRadius) throw new ArgumentException("Radius must be a decimal number");
                newSphere.Radius = radius;
            }
            catch (ArgumentException argEx)
            {
                lblNewShapeRadiusException.Visible = true;
                lblNewShapeRadiusException.Text = argEx.Message;
            }
            catch (NonPositiveRadiusException negRadEx)
            {
                lblNewShapeRadiusException.Visible = true;
                lblNewShapeRadiusException.Text = negRadEx.Message;
            }
        }
    }
}