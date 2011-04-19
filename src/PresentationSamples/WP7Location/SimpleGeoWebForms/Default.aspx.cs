using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SimpleGeoWebForms.Models;

namespace SimpleGeoWebForms {
    public partial class Default : System.Web.UI.Page {       
        
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void bntSumbit_Click(object sender, EventArgs e) {
            try {

                var client = new Client("vvc2y7nAjkx6fUaJqQ94FT7nAdZCWQrA", "CJYFj8Sy3WwDL2sFfQXJnDdyXh7BqDU2");
                var featureCollection = client.GetNearbyPlaces(double.Parse(txtLatitude.Text), double.Parse(txtLongitude.Text), txtSearch.Text);
                if (featureCollection.Features.Count > 0) {
                    rptPlaces.DataSource = featureCollection.Features;
                    rptPlaces.DataBind();
                    rptPlaces.Visible = true;
                    lblMessage.Visible = false;
                } else {
                    lblMessage.Text = "No places found";
                    lblMessage.Visible = true;
                }


            } catch (Exception ex) {
                lblMessage.Text = ex.GetBaseException().Message;
                lblMessage.Visible = true;
            }
        }
    }
}