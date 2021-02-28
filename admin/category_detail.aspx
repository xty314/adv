<%@ Page Language="C#" AutoEventWireup="true" CodeFile="category_detail.aspx.cs" Inherits="admin_src_category_detail" MasterPageFile="Master_Page/Admin_Layout/AdminLayout.master" %>
<%@ Assembly Src="common/Common.cs" %>
<asp:Content ContentPlaceHolderID="AdditionalCSS" runat="server">
    <link href="src/plugins/dropzone/dropzone.css" rel="stylesheet" />
    <link rel="stylesheet" href="src/plugins/bootstrap-datepicker/datepicker.css">
</asp:Content>

<asp:Content ContentPlaceHolderID="Header" runat="server">
    <div class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1 class="m-0 text-dark" id="Title1"><%=title %></h1>
                </div>
                <!-- /.col -->
                <div class="col-sm-6">
                    <div class="float-right">
                        <button class="btn bg-blue save-btn" data-toggle="modal"
                            data-target="#SaveCategoryModal">
                            Save</button>
                        <button class="btn bg-blue add-btn" data-toggle="modal" data-target="#UploadPicModal">
                            <i
                                class="fa fa-file"></i>Upload new image</button>
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
        <form method="post" id="MainForm">
            <div class="row connectedSortable">
                <%foreach (string file in files)
                    {
                        string fileName = Common.GetLastDir(file);
                        PicName pn = new PicName(fileName);%>
                <div class="col-md-2  ui-sortable img-file">
                    <!-- Box Comment -->
                    <div class="card card-widget">
                        <div class="card-header ui-sortable-handle <%=BackgroundColor(pn) %>" style="cursor: move;">
                            <h3 class="card-title ">
                                <i class="fas fa-file"></i>
                              
                                <%=pn.ShowName() %>
                            </h3>

                        </div>
                        <div class="card-body ">
                   
                            <img class="img-fluid pad mb-1" src="<%=file %>" />

                            <button type="button" data-name="<%=fileName%>" data-toggle="modal" data-target="#CopyPicModal" class=" copy-btn btn btn-default btn-sm">
                                <i
                                    class="fas fa-copy"></i>Copy</button>
                            <button type="button" data-name="<%=fileName%>"
                                data-src="<%=file%>"
                                data-toggle="modal" data-target="#MovePicModal" class="move-btn btn btn-default btn-sm">
                                <i
                                    class="fas fa-share"></i>Move</button>

                        </div>
                        <!-- /.card-body -->
                        <!-- /.card-footer -->
                        <div class="card-footer">

                            <!-- .img-push is used to add margin to elements next to floating images -->
                            <div class="img-push">
                                <div class="form-group row">
                                    <input type="checkbox" class="form-control form-control-sm col-sm-1" value="true"
                                        name="<%=fileName%>_delete" />
                                    <label for="recipient-name" class="col-form-label col-sm-3">Del</label>
                                    <label for="recipient-name" class="col-form-label col-sm-3 text-right">Time</label>
                                    <div class="input-group  col-sm-5">
                                        <input type="text" class="form-control " name="<%=fileName%>_time"
                                            value="<%=Common.GetImgSecond(fileName) %>" />
                                        <div class="input-group-append">
                                            <div class="input-group-text">S</div>
                                        </div>
                                    </div>

                                    <input type="hidden" class="seq" name="<%=fileName%>_seq"
                                        value="<%=fileName.Substring(0,4)%>" />
                                </div>
            
                                <div class="form-group">
                          

                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">
                                                <i class="far fa-calendar-alt"></i>
                                            </span>
                                        </div>
                                        <input type="text" class="form-control pull-right datepicker" name="<%=fileName%>_start"  value="<%=pn.StartDate %>" >
                                         <div class="input-group-prepend">
                                            <span class="input-group-text">
                                                To
                                            </span>
                                        </div>
                                        <input type="text" class="form-control pull-right datepicker"   name="<%=fileName%>_end"  value="<%=pn.EndDate %>" >
                                      
                                    </div>
                                    <!-- /.input group -->
                                </div>
                            </div>

                        </div>
                        <!-- /.card-footer -->
                    </div>
                    <!-- /.card -->
                </div>

                <%} %>
            </div>
        </form>
    </section>
    <!-- /.content -->
    <div class="modal fade" id="SaveCategoryModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
        aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel"><%=title %></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    Are you sure to save the changes?
                    <%-- <input type="hidden" class="form-control  col-sm-10" name='company' id="s_new_sscat">--%>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <button type="submit" name='cmd' value='save' form="MainForm" class="btn btn-primary">Save</button>
                </div>
            </div>
        </div>
    </div>


    <div class="modal fade" id="UploadPicModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
        aria-hidden="true">
        <div class="modal-dialog modal-xl" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="CompanyCategoryModalHeader">Upload Images</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <form action="category_detail.aspx?cat=<%=title %>" class="dropzone" id='UploadPicForm'>
                    </form>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <button type="button" id="UploadModalBtn" class="btn btn-primary">Refresh</button>
                </div>
            </div>
        </div>
    </div>

    <form action="category_detail.aspx?cat=<%=title%>" method="post">
        <div class="modal fade" id="CopyPicModal" tabindex="-1" role="dialog"
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
                            <label>Picture Copy To</label>
                            <select class="duallistbox" multiple="multiple" name="categories">
                                <%foreach (string cat in allCategoryDirs)
                                    {
                                %>

                                <option value="<%=Common.GetLastDir(cat) %>"><%=Common.GetLastDir(cat) %></option>
                                <%} %>
                            </select>
                            <input type="hidden" name="file">
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        <button type="submit" name='cmd' id="SelectModalBtn" class="btn btn-primary" value="copy">Save</button>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <form action="category_detail.aspx?cat=<%=title%>" method="post">
        <div class="modal fade" id="MovePicModal" tabindex="-1" role="dialog"
            aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-md" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="CompanyCategoryModalHeader">Move Picture To</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">

                        <div class="form-group">
                            <img class="img-fluid pad mb-1" id="move_pic" />
                            <select name="category" class="form-control">
                                <%foreach (string cat in allCategoryDirs)
                                    {
                                        if (Common.GetLastDir(cat) != title)
                                        {%>
                                <option value="<%=Common.GetLastDir(cat) %>"><%=Common.GetLastDir(cat) %></option>
                                <%}
                                    } %>
                            </select>
                            <input type="hidden" name="file">
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        <button type="submit" name='cmd' id="SelectModalBtn" class="btn btn-primary" value="move">Save</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ContentPlaceHolderID="AdditionalJS" runat="server">
    <script src="src/plugins/dropzone/dropzone.js"></script>
    <script src="src/plugins/bootstrap4-duallistbox/jquery.bootstrap-duallistbox.min.js"></script>
    <script src="src/plugins/moment/moment.min.js"></script>
    <script src="src/plugins/bootstrap-datepicker/datepicker.js"></script>

    <script src="src/js/category_detail.js"></script>
    
</asp:Content>
