<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MakeID.ascx.cs" Inherits="GIBS.Modules.FBClients.MakeID" %>

<style>
    

.button-group, .play-area {
  border: 1px solid grey;
  padding: 1em 1%;
  margin-bottom: 1em;
  text-align: center;
}

 .hover-zoom {
    -moz-transition:all 0.3s;
    -webkit-transition:all 0.3s;
     transition:all 0.3s
 }
.hover-zoom:hover {
    -moz-transform: scale(1.1);
    -webkit-transform: scale(1.1);
     transform: scale(4.6)
 }

.btn.btn-success {
  display: inline-block;
padding: 10px 20px;
color: #fff;
background-color: #5cb85c;

text-align:center;
text-decoration:none;
border-radius: 5px;
}

.btn.btn-success:hover {
  color: #fff;
  background-color: #70db70;
  border-color: #000;
  outline: none;
  box-shadow: inset;
}

</style>
<div style="padding: 20px;"> &nbsp;</div>

 <h3 style="text-align: center;"><asp:Label ID="LabelClientInfo" runat="server" Text="LabelClientInfo"></asp:Label></h3>


<div class="button-group">
<asp:Button ID="Button1" runat="server" Text="Generate ID Card PDF" CssClass="btn"
    onclick="Button1_Click" />
</div>

<div style="text-align: center; padding-top:20px; padding-bottom:20px;"><asp:HyperLink ID="HyperLinkPDF" Visible="false" runat="server" CssClass="btn btn-success" Target="_blank">SUCCESS - View PDF</asp:HyperLink></div>



<div style="padding-top:20px; width: 100%; text-align: center;"> 
<asp:Image ID="ImageIDClient" runat="server" Height="100" CssClass="hover-zoom" />
    <asp:HiddenField ID="HiddenFieldClientPicture" runat="server" />
</div>


<div class="button-group">
  
 
    <asp:Button ID="ButtonReturnToClientManager" runat="server" Text="Return To Client Manager" OnClick="ButtonReturnToClientManager_Click" />

    
    
</div>
