<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClientMerge.ascx.cs" Inherits="GIBS.Modules.FBClients.ClientMerge" %>

<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<%@ Register TagPrefix="dnn" TagName="Audit" Src="~/controls/ModuleAuditControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="SectionHead" Src="~/controls/SectionHeadControl.ascx" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<dnn:DnnCssInclude ID="DnnCssInclude2" runat="server" FilePath="~/DesktopModules/GIBS/FBClients/Style.css" />
<dnn:DnnCssInclude ID="DnnCssInclude3" runat="server" FilePath="https://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/smoothness/jquery-ui.css" />

<div class="dnnFormMessage dnnFormInfo">
<asp:Label ID="lblMsg" runat="server" Text="Search for record to MERGE with the one listed... " /></div>

<div class="dnnForm" id="form-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#Client">Merge Target Client</a></li>
    </ul>
    <div id="Client" class="dnnClear">
        <asp:Label runat="server" ID="lblFormMessage" CssClass="dnnFormMessage dnnFormInfo" ResourceKey="lblFormMessage" Visible="False"/>
           
  	            <asp:GridView ID="GridViewSearchMaster" runat="server" CssClass="mGrid" 
                    DataKeyNames="ClientID"  PageSize="100" AllowPaging="True" AutoGenerateColumns="False" EnableViewState="true"
                    resourcekey="GridViewSearch" EmptyDataText="No Records Found">
                    <PagerStyle CssClass="pgr" />  
                    <PagerSettings Mode="NumericFirstLast" /> 
                    <Columns>
                        <asp:BoundField HeaderText="ID" DataField="ClientID"></asp:BoundField>
                        <asp:BoundField HeaderText="Last Name" DataField="ClientLastName" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                        <asp:BoundField HeaderText="First Name" DataField="ClientFirstName"></asp:BoundField>
                        <asp:BoundField HeaderText="MI" DataField="ClientMiddleInitial"></asp:BoundField>
                        <asp:BoundField HeaderText="Suffix" DataField="ClientSuffix" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                        <asp:BoundField HeaderText="Address" DataField="ClientAddress"></asp:BoundField>
                        <asp:BoundField HeaderText="Village" DataField="ClientTown" ></asp:BoundField>
                        <asp:BoundField HeaderText="Age" DataField="ClientAge" ItemStyle-HorizontalAlign="Center" ></asp:BoundField>
                        <asp:BoundField HeaderText="DOB" DataField="ClientDOB" DataFormatString="{0:MM/dd/yyyy}" ItemStyle-HorizontalAlign="Center" Visible="True" ></asp:BoundField>
                        <asp:TemplateField HeaderText="Verified" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Image ID="Image1" runat="server" ImageUrl='<%# (Eval("ClientDOBVerify").Equals(true) ? "~/DesktopModules/GIBS/FBClients/Images/yes.png" : "~/DesktopModules/GIBS/FBClients/Images/no.png")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>


                </Columns>
            </asp:GridView>         
           
            <div class="dnnFormItem dnnFormHelp dnnClear">
                <p class="dnnFormRequired"><asp:Label ID="lblRequiredFields" runat="server" ResourceKey="Required Indicator" Visible="False" /></p>
            </div>
            <div style="position:relative;float:right;padding-right:30px;">
                <asp:Button ID="btnSearch1" runat="server" ResourceKey="btnSearch" onclick="btnSearch_Click" CssClass="dnnPrimaryAction" />
                <asp:LinkButton runat="server" ID="btnSearch" CssClass="dnnPrimaryAction" 
                    ResourceKey="btnSearch" onclick="btnSearch_Click" Visible="False" />
                <br />
            </div>

        <fieldset>
        <div class="dnnFormItem">
            <dnn:Label runat="server" ControlName="txtClientId" ID="lblClientId" Suffix=":" ResourceKey="lblClientId" />
            <asp:TextBox runat="server" ID="txtClientId" />
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
        
        
        
  <div>
            <asp:GridView ID="GridViewSearchChild" runat="server" CssClass="mGrid" 
                    DataKeyNames="ClientID"  OnRowEditing="GridViewSearch_RowEditing" 
                    PageSize="100" AllowPaging="True" AutoGenerateColumns="False" 
                    resourcekey="GridViewSearch" EmptyDataText="No Records Found" >
                    <PagerStyle CssClass="pgr" />  
                    <PagerSettings Mode="NumericFirstLast" /> 
                    <Columns>
           
                    <asp:TemplateField HeaderText=""   >
                        <ItemTemplate >
                             <a href='<%#DotNetNuke.Common.Globals.NavigateURL("EditClient","cid=" + Eval("ClientID"),"mid=" + ModuleId) %>' target='_blank'  >
                                VIEW
                            </a>
                          <!---   <a href="javascript:dnnModal.show('<%#DotNetNuke.Common.Globals.NavigateURL("EditClient","cid=" + Eval("ClientID").ToString(),"mid=" + ModuleId) + "&popUp=true" %>',false,550,950,true)" >
                                view
                            </a>-->
                            
                            </ItemTemplate>
                    </asp:TemplateField>

                     
           
                        <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                            
                                <asp:Button GroupName="ChildMerge" Text="Merge"  ID="btnChildMerge"  CausesValidation="False"     
                                    CommandArgument='<%# Eval("ClientID") %>'  
                                    OnClick="GridViewSearchChild_SelectedIndexChanged" runat="server">
                                </asp:Button>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField HeaderText="ID" DataField="ClientID"></asp:BoundField>
                        <asp:BoundField HeaderText="Last Name" DataField="ClientLastName" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                        <asp:BoundField HeaderText="First Name" DataField="ClientFirstName"></asp:BoundField>
                        <asp:BoundField HeaderText="MI" DataField="ClientMiddleInitial"></asp:BoundField>
                        <asp:BoundField HeaderText="Suffix" DataField="ClientSuffix" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                        <asp:BoundField HeaderText="Address" DataField="ClientAddress"></asp:BoundField>
                        <asp:BoundField HeaderText="Village" DataField="ClientTown" ></asp:BoundField>
                        <asp:BoundField HeaderText="Age" DataField="ClientAge" ItemStyle-HorizontalAlign="Center" ></asp:BoundField>
                        <asp:BoundField HeaderText="DOB" DataField="ClientDOB" DataFormatString="{0:MM/dd/yyyy}" ItemStyle-HorizontalAlign="Center" Visible="True" ></asp:BoundField>
                        <asp:TemplateField HeaderText="Verified" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Image ID="Image1" runat="server" ImageUrl='<%# (Eval("ClientDOBVerify").Equals(true) ? "~/DesktopModules/GIBS/FBClients/Images/yes.png" : "~/DesktopModules/GIBS/FBClients/Images/no.png")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
           
 
                </Columns>
            </asp:GridView>	
            
        </div>
    </fieldset>
</div>
<asp:HiddenField ID="hidClientIDMaster" runat="server" />
</div>