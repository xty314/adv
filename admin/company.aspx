<%@ Page Language="C#" AutoEventWireup="true" CodeFile="company.aspx.cs" Inherits="v2_invoice" MasterPageFile="./Master_Page/Admin_Layout/AdminLayout.master" %>
<%@ Assembly Src="./common/Common.cs" %>


<%--注意加入MasterPageFile--%>



<%--<%@Import Namespace="System.Data.SqlClient" %>
<%@Import Namespace ="System.Data" %>--%>
<asp:Content ContentPlaceHolderID="AdditionalCSS" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="Header" runat="server">
    <div class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1 class="m-0 text-dark" id="Title1">Company</h1>
                </div>
                <!-- /.col -->
                <div class="col-sm-6">
                    <div class="float-right">
                        <button class="btn bg-blue" data-toggle="modal" data-target="#NewCompanyModal"><i
                                class="fa fa-pen"></i> Add New Company </button>
                        <%-- <button type="button" class="btn btn-success"><i class="fa fa-download"></i>Export </button>--%>
                    </div>
                </div>
                <!-- /.col -->
            </div>
            <!-- /.row -->
        </div>
        <!-- /.container-fluid -->
    </div>

</asp:Content>
<asp:Content ContentPlaceHolderID="Content" runat="server">

    <!-- Main content -->
    <section class="content">

        <!-- Default box -->
        <div class="card">

            <div class="card-body p-0">
                <table class="table table-striped table-hover projects">
                    <thead>
                        <tr>
                            <th style="width: 1%">
                                #
                            </th>
                            <th style="width: 20%">
                                Company
                            </th>
                            <th style="width: 30%">
                                Url
                            </th>
                            <th class="text-center">
                                Categories
                            </th>

                            <th style="width: 20%" class="text-right">
                                Action
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <%for(int i=0;i<companyDirs.Count;i++){
                         
                              cats = Common.GetCategories(companyDirs[i].ToString());%>
                        <tr>
                            <td>
                                #
                            </td>
                            <td>
                                <a>
                                    <%=companyDirs[i].ToString() %>
                                </a>
                                <br>
                                <small>
                                    Created <%=companyDirs[i].CreationTime %>
                                </small>
                            </td>
                            <td>
                                <a href="http://adv.gcloud.co.nz/adv/<%=companyDirs[i].ToString() %>"
                                    target="_blank">http://adv.gcloud.co.nz/adv/<%=companyDirs[i].ToString() %></a>
                            </td>
                            <td class="project_progress  text-center">
                                <%foreach (string cat in cats){ %>
                                <span class="badge badge-success text-lg mt-1"><a
                                        href="category_detail.aspx?cat=<%=cat %>"
                                        style="color:white"><%=cat %></a></span>

                                <%} %>
                            </td>

                            <td class="project-actions text-right">
                                <a class="btn btn-primary btn-sm select-btn" href="#" data-toggle="modal"
                                    data-target="#CompanyCategoryModal" data-categories='<%=string.Join(",",cats) %>'
                                    data-company="<%=companyDirs[i].ToString()%>">
                                    <i class="fas fa-folder">
                                    </i>
                                    Categories
                                </a>
                                <a class="btn btn-info btn-sm rename-btn" href="#" data-toggle="modal"
                                    data-target="#RenameCompanyModal" data-company="<%=companyDirs[i].ToString()%>">
                                    <i class="fas fa-pencil-alt">
                                    </i>
                                    Rename
                                </a>
                                <%if (!undeleteCompanyList.Contains(companyDirs[i].ToString()))
                               { %>
                                <a class="btn btn-danger btn-sm delete-btn" href="#" data-toggle="modal"
                                    data-target="#DeleteCompanyModal" data-company="<%=companyDirs[i].ToString()%>">
                                    <i class="fas fa-trash">
                                    </i>
                                    Delete
                                </a>
                                <%} %>
                            </td>
                        </tr>
                        <%  } %>

                    </tbody>
                </table>

            </div>
            <!-- /.card-body -->
        </div>
        <!-- /.card -->

    </section>
    <!-- /.content -->
    <form action="company.aspx" method="post" id='CompanyCategoryForm'>
        <div class="modal fade" id="CompanyCategoryModal" tabindex="-1" role="dialog"
            aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-xl" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="CompanyCategoryModalHeader"></h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">

                        <div class="form-group">
                            <label>Category List</label>
                            <select class="duallistbox" multiple="multiple" name="categories">
                                <%foreach (string cat in allCategoryDirs)
                                    {%>
                                <option value="<%=Common.GetLastDir(cat) %>"><%=Common.GetLastDir(cat) %></option>
                                <%} %>

                            </select>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        <button type="submit" name='select' id="SelectModalBtn" class="btn btn-primary">Save</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <form action="company.aspx" method="post" id='NewCompanyForm'>
        <div class="modal fade" id="NewCompanyModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
            aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">New Company</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">

                        <div class="form-group row">
                            <label for="recipient-name" class="col-form-label col-sm-4">Company Name:</label>
                            <input type="text" class="form-control  col-sm-8" name='company'>
                        </div>
                        <%-- <input type="hidden" class="form-control  col-sm-10" name='company' id="s_new_sscat">--%>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        <button type="submit" name='cmd' value='addNewCompany' class="btn btn-primary">Save</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <form id="RenameCompanyForm" action="company.aspx" method="post">
        <div class="modal fade" id="RenameCompanyModal" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="RenameModalHeader"></h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">

                        <div class="form-group row">
                            <label for="recipient-name" class="col-form-label col-sm-6">New Company Name:</label>
                            <input type="text" class="form-control  col-sm-6" name='new_company_name' id="s_new_scat">
                        </div>
                        <%-- <input type="hidden" class="form-control  col-sm-10" name='company' id="s_new_sscat">--%>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        <button type="submit" name='rename' id="RenameModalBtn" class="btn btn-primary">Save</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <form action="company.aspx" method="post" id='DeleteCompany'>
        <div class="modal fade" id="DeleteCompanyModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
            aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content bg-danger">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Delete Company</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body" id="DeleteModalBody">

                        Delete Company
                        <%-- <input type="hidden" class="form-control  col-sm-10" name='company' id="s_new_sscat">--%>
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        <button type="submit" name='delete' class="btn btn-primary" id="DeleteModalBtn">Delete</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ContentPlaceHolderID="AdditionalJS" runat="server">
    <script src="src/plugins/bootstrap4-duallistbox/jquery.bootstrap-duallistbox.min.js"></script>
    <script src="src/js/company.js"></script>

</asp:Content>