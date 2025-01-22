using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParkingSystem.BLL; // Include BLL for business logic
using ParkingSystem.DAL; // Include DAL for data access

namespace ParkingManagentSystem
{
    public partial class ParkingSystem : System.Web.UI.Page
    {
        private readonly ParkingService _parkingService;

        public ParkingSystem()
        {
            _parkingService = new ParkingService(); // Initialize ParkingService (BLL)
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGates();
            }
        }

        private void LoadGates()
        {
            var gates = _parkingService.GetAllGates();
            ddlGate.DataSource = gates;
            ddlGate.DataTextField = "GateName";
            ddlGate.DataValueField = "GateId";
            ddlGate.DataBind();
            ddlGate.Items.Insert(0, new ListItem("Select Gate", "0"));
        }

        protected void btnParkVehicle_Click(object sender, EventArgs e)
        {
            try
            {
                int SpaceId = int.Parse(ddlSpace.SelectedValue);
                string vehicleNumber = txtVehicleNumber.Text;

                if (SpaceId == 0 || string.IsNullOrWhiteSpace(vehicleNumber))
                {
                    ShowMessage("Please select a gate and enter a vehicle number.");
                    return;
                }

                int parkingID = _parkingService.ParkVehicle(vehicleNumber, SpaceId);
                ShowMessage($"Vehicle {vehicleNumber} parked successfully at Space ID {SpaceId} with parking id {parkingID}.");
            }
            catch (Exception ex)
            {
                ShowMessage($"Error: {ex.Message}");
            }
        }
        
        public void loadSpaceInfo(object sender, EventArgs e)
        {
            try
            {
                var gateId = int.Parse(ddlGate.SelectedValue);
                var space = _parkingService.GetAvailableSpacesForGate(gateId);
                ddlSpace.DataSource = space;
                ddlSpace.DataTextField = "SpaceId";
                ddlSpace.DataValueField = "SpaceId";
                ddlSpace.DataBind();
                ddlSpace.Items.Insert(0, new ListItem("Select SpaceId", "0"));
            }
            catch (Exception ex)
            {
                ShowMessage($"Error: {ex.Message}");
            }
        }
        protected void btnUnparkVehicle_Click(object sender, EventArgs e)
        {
            try
            {
                int parkingId = int.Parse(txtParkingId.Text);

                _parkingService.UnparkVehicle(parkingId);
                var charges = _parkingService.CalculateCharges(parkingId);

                ShowMessage($"Vehicle unparked successfully. Total charges: {charges:C}");
            }
            catch (Exception ex)
            {
                ShowMessage($"Error: {ex.Message}");
            }
        }

        private void ShowMessage(string message)
        {
            lblMessage.Text = message;
            lblMessage.Visible = true;
        }
    }
}