<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportChristmas.ascx.cs" Inherits="GIBS.Modules.FBClients.ReportChristmas" %>

<h1 style="text-align:center;">The Family Pantry of Cape Cod<br />Christmas Toys Ticket</h1>



<div style="padding-bottom:10px;text-align:center;">


<asp:Button ID="PrintButton" runat="server" Text="Print Page" OnClientClick="javaScript:window.print(); return false;" />&nbsp;&nbsp;
<asp:Button ID="CloseButton" runat="server" Text="Close Window" OnClientClick="javaScript:closeWindow();" />

</div>

<div style="text-align: center;"><asp:Label ID="lblMessage" runat="server" Width="560px" Text="" CssClass="dnnFormMessage dnnFormSuccess" Visible="True"></asp:Label></div>



<asp:GridView ID="gvAFM" runat="server" HorizontalAlign="Center" EmptyDataText="NO ELIGIBLE/VERIFIED CHILDREN" 
    DataKeyNames="VisitID"        
    AutoGenerateColumns="False" CssClass="dnnGrid">
    
<PagerStyle CssClass="pgr" />  
<PagerSettings Mode="NumericFirstLast" /> 
    <Columns>


        <asp:TemplateField HeaderText="Verified for Toys" ItemStyle-HorizontalAlign="Center" >
            <ItemTemplate>
            <asp:Image ID="Image1" runat="server" ImageUrl='<%# (Eval("VerifiedToys").Equals(true) ? "~/DesktopModules/GIBS/FBClients/Images/yes.png" : "~/DesktopModules/GIBS/FBClients/Images/no.png")%>' />
            </ItemTemplate>
        </asp:TemplateField>


        <asp:BoundField HeaderText="First Name" DataField="ClAddFamMemFirstName" ItemStyle-VerticalAlign="Top" ></asp:BoundField>
        <asp:BoundField HeaderText="MI" DataField="AFMMiddleInitial" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center" ></asp:BoundField>
        <asp:BoundField HeaderText="Last Name" DataField="ClAddFamMemLastName" ItemStyle-VerticalAlign="Top" ></asp:BoundField>
        
		<asp:BoundField HeaderText="DOB" ItemStyle-VerticalAlign="Top" DataField="ClAddFamMemDOB" DataFormatString="{0:d}" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center"></asp:BoundField>

        <asp:BoundField HeaderText="Age" DataField="AFM_Age" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center" ></asp:BoundField>
  
        <asp:BoundField HeaderText="Gender" DataField="AFMGender" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center" ></asp:BoundField>
      
         <asp:BoundField HeaderText="Sizes-Notes" DataField="xMasSizes" ItemStyle-VerticalAlign="Top" ></asp:BoundField>

        <asp:TemplateField HeaderText="Bike Raffle*" ItemStyle-HorizontalAlign="Center" >
            <ItemTemplate>
            <asp:Image ID="Image2" runat="server" ImageUrl='<%# (Eval("BikeRaffle").Equals(true) ? "~/DesktopModules/GIBS/FBClients/Images/yes.png" : "~/DesktopModules/GIBS/FBClients/Images/no.png")%>' />
            </ItemTemplate>
        </asp:TemplateField>


    </Columns>
</asp:GridView>	


<asp:Literal ID="LiteralToyTicketContent" runat="server"></asp:Literal>




<p style="width: 100%; padding:8px 0px 8px 0px;text-align:center;position: fixed; bottom: 0; margin: 0 auto;"><asp:Label ID="lblPrintDate" runat="server" Text="Label"></asp:Label></p>



<script type = "text/javascript">

    function closeWindow() {

        window.open('', '_self', '');
        window.close();
    } 


</script> 
