<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewFBClients.ascx.cs" Inherits="GIBS.Modules.FBClients.ViewFBClients" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

<dnn:DnnCssInclude ID="DnnCssInclude1" runat="server" FilePath="~/DesktopModules/GIBS/FBClients/Style.css?1=1" />

<script type="text/javascript" language="javascript" >

    // BIND DNN Tabs
    jQuery(function ($) { $('#form-demo').dnnTabs(); });

</script>


<div class="dnnForm" id="form-demo">

        <ul class="dnnAdminTabNav">
            <li><a href="#Client">Search Clients</a></li>
            <li><a href="#AFM">Search Household Members</a></li>

        </ul>

        <div style="text-align:right; padding-right:35px;"><asp:CheckBox ID="cbxIncludeInactive" runat="server" Text="Include Inactive" TextAlign="Right" />
        </div>
<div id="Client" class="dnnClear">

    <asp:Label runat="server" ID="lblFormMessage" CssClass="dnnFormMessage dnnFormInfo" ResourceKey="lblFormMessage" Visible="False"/>
    <div class="dnnFormItem dnnFormHelp dnnClear">
        <p class="dnnFormRequired"><asp:Label ID="lblRequiredFields" runat="server" ResourceKey="Required Indicator" Visible="False" /></p>
    </div>


 <div style="position:relative;float:right;padding-right:30px;">
     <asp:Button ID="btnSearch1" runat="server" ResourceKey="btnSearch" onclick="btnSearch_Click" CssClass="btn btn-primary" />
 <asp:LinkButton runat="server" ID="btnSearch" CssClass="dnnPrimaryAction" 
                ResourceKey="btnSearch" onclick="btnSearch_Click" Visible="False" />
                
<br /><br />

<asp:LinkButton ID="btnAddNewClient" runat="server" CssClass="btn btn-default" 
                ResourceKey="btnAddNewClient" onclick="btnAddNewClient_Click" CausesValidation="False" />                
                
                </div>
        <div style="position:relative;float:right;padding-right:30px;"></div>
                
    <fieldset>

         <div class="dnnFormItem">
                   <dnn:label id="lblClientType" runat="server" resourcekey="lblClientType" controlname="ddlClientType" suffix=":" />
                   <asp:DropDownList ID="ddlClientType" runat="server" TabIndex="1">
                        <asp:ListItem Text="-" Value=""></asp:ListItem>
                        <asp:ListItem Text="Individual"></asp:ListItem>
                        <asp:ListItem Text="Group Home"></asp:ListItem>
                       <asp:ListItem Text="Pallet"></asp:ListItem>
                    </asp:DropDownList>
        </div>

        <div class="dnnFormItem">
            <dnn:Label runat="server" ControlName="txtClientId" ID="lblClientId" Suffix=":" ResourceKey="lblClientId" />
            <asp:TextBox runat="server" ID="txtClientId" /><asp:RangeValidator runat="server" Type="Integer"  ID="ClientIDRangeValidator" 
MinimumValue="0" MaximumValue="999999" ControlToValidate="txtClientId" ErrorMessage="<span style='color:red'>Value must numeric</span>" />
        </div>       
       
        <div class="dnnFormItem">
            <dnn:Label runat="server" ID="lblFirstName" ControlName="txtFirstName" ResourceKey="lblFirstName" Suffix=":" />
            <asp:TextBox runat="server" ID="txtFirstName" />
            
        </div>


        <div class="dnnFormItem">
            <dnn:Label runat="server" ID="lblLastName" ControlName="txtLastName" ResourceKey="lblLastName" Suffix=":" />
            <asp:TextBox runat="server" ID="txtLastName" />
            
        </div>

        <div class="dnnFormItem">
            <dnn:Label runat="server" ID="lblAddress" ControlName="txtAddress" ResourceKey="lblAddress" Suffix=":" />
            <asp:TextBox runat="server" ID="txtAddress" MaxLength="50" />
            
        </div>
		
        <div class="dnnFormItem">
            <dnn:Label runat="server" ID="lblCity" ControlName="ddlCity" ResourceKey="lblCity" Suffix=":" />
            <asp:DropDownList ID="ddlCity" runat="server"></asp:DropDownList>
            
        </div>	

        <div class="dnnFormItem" id="divClientIDCard" runat="server">
            <dnn:Label runat="server" ControlName="txtClientIdCard" ID="lblClientIdCard" Suffix=":" ResourceKey="lblClientIdCard" />
            <asp:TextBox runat="server" ID="txtClientIdCard" />
        </div>



    </fieldset>





</div>

    <div id="AFM" class="dnnClear">

        <div style="position:relative;float:right;padding-right:30px;">
             <asp:Button ID="btnSearchAFM" runat="server" ResourceKey="btnSearchAFM" onclick="btnSearchAFM_Click" CssClass="dnnPrimaryAction" />
        </div>
            <fieldset>
                <div class="dnnFormItem">
                    <dnn:Label runat="server" ID="lblFirstNameAFM" ControlName="txtFirstNameAFM" ResourceKey="lblFirstNameAFM" Suffix=":" />
                    <asp:TextBox runat="server" ID="txtFirstNameAFM" />
                </div>

                <div class="dnnFormItem">
                    <dnn:Label runat="server" ID="lblLastNameAFM" ControlName="txtLastNameAFM" ResourceKey="lblLastNameAFM" Suffix=":" />
                    <asp:TextBox runat="server" ID="txtLastNameAFM" />
                </div>

            </fieldset>

    </div>

    <!----
OnRowDataBound="GridViewSearch_RowDataBound" 
        ---->
<asp:GridView ID="GridViewSearch" runat="server" CssClass="table table-striped table-bordered table-list" OnPageIndexChanging="GridViewSearch_PageIndexChanging" 
 
    DataKeyNames="ClientID" OnRowEditing="GridViewSearch_RowEditing" PageSize="10" AllowPaging="True"     
    AutoGenerateColumns="False" 
    
    resourcekey="GridViewSearch" EmptyDataText="No Records Found">

<PagerStyle CssClass="pagination-ys" />  
<PagerSettings Mode="NumericFirstLast" /> 
    <Columns>




        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" >
         <ItemTemplate>
           <asp:LinkButton ID="LinkButtonEdit" CausesValidation="False"     
             CommandArgument='<%# Eval("ClientID") %>' 
             CommandName="Edit" runat="server"><asp:image ID="imgEdit" runat="server" imageurl="~/DesktopModules/GIBS/FBClients/Images/edit-32.png" AlternateText="Edit Record" />
             </asp:LinkButton>
         </ItemTemplate>
       </asp:TemplateField>

 
<asp:TemplateField HeaderText="Verified" ItemStyle-HorizontalAlign="Center" >
<ItemTemplate>

<asp:Image ID="Image1" runat="server" ImageUrl='<%# (Eval("ClientDOBVerify").Equals(true) ? "~/DesktopModules/GIBS/FBClients/Images/yes.png" : "~/DesktopModules/GIBS/FBClients/Images/no.png")%>' />


</ItemTemplate>
</asp:TemplateField>


       <asp:BoundField HeaderText="ID" DataField="ClientID"></asp:BoundField>

        <asp:BoundField HeaderText="Last Name" DataField="ClientLastName" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
        <asp:BoundField HeaderText="First Name" DataField="ClientFirstName"></asp:BoundField>
        <asp:BoundField HeaderText="MI" DataField="ClientMiddleInitial"></asp:BoundField>
        
        <asp:BoundField HeaderText="Suffix" DataField="ClientSuffix" ItemStyle-HorizontalAlign="Left" Visible="False"></asp:BoundField>
        <asp:BoundField HeaderText="Address" DataField="ClientAddress"></asp:BoundField>
        
        <asp:BoundField HeaderText="Unit" DataField="ClientUnit"></asp:BoundField>
        <asp:BoundField HeaderText="Village" DataField="ClientTown" ></asp:BoundField>

        <asp:BoundField HeaderText="Age" DataField="ClientAge" ItemStyle-HorizontalAlign="Center" ></asp:BoundField>
       
        <asp:BoundField HeaderText="DOB" DataField="ClientDOB" DataFormatString="{0:MM/dd/yyyy}" ItemStyle-HorizontalAlign="Center" Visible="True" ></asp:BoundField>

        <asp:TemplateField HeaderText="Active" ItemStyle-HorizontalAlign="Center" >
            <ItemTemplate><div style="display:inline;">
                <asp:Image ID="ImageIsLocked" ImageUrl="~/DesktopModules/GIBS/FBClients/Images/Lock.png" ToolTip="Locked Account" AlternateText="Locked Account" Visible='<%# Eval("IsLocked") %>' runat="server" />
                <asp:Image ID="Image122" runat="server" AlternateText="Is Active" ImageUrl='<%# (Boolean.Parse(Eval("IsActive").ToString()) ? "~/DesktopModules/GIBS/FBClients/Images/yes.png" : "~/DesktopModules/GIBS/FBClients/Images/no.png") %>' />
                
                </div>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:BoundField HeaderText="Interviewer" DataField="CaseWorkerName" Visible="False"></asp:BoundField>
        <asp:BoundField HeaderText="Entry Date" DataField="CreatedOnDate" Visible="False" DataFormatString="{0:MM/dd/yyyy}" ItemStyle-HorizontalAlign="Center"></asp:BoundField>

        <asp:BoundField HeaderText="AFM First Name" DataField="ClAddFamMemFirstName" ItemStyle-HorizontalAlign="Left" ></asp:BoundField>
        <asp:BoundField HeaderText="AFM Last Name" DataField="ClAddFamMemLastName" ItemStyle-HorizontalAlign="Left" ></asp:BoundField>
        
        <asp:BoundField HeaderText="Last Visit" DataField="visitdate" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundField>
        <asp:BoundField HeaderText="Bags" DataField="VisitNumBags" ItemStyle-HorizontalAlign="Center"></asp:BoundField>

    </Columns>
</asp:GridView>	


</div>