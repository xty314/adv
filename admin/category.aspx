<%@ Page Language="C#" AutoEventWireup="true" CodeFile="category.aspx.cs" Inherits="admin_category" MasterPageFile="./Master_Page/Admin_Layout/AdminLayout.master"  %>

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
                    <h1 class="m-0 text-dark" id="Title1">Category</h1>
                </div>
                <!-- /.col -->
                <div class="col-sm-6">
                    <div class="float-right">
                        <button class="btn bg-blue add-btn" data-toggle="modal" data-target="#NewCategoryModal"><i class="fa fa-pen"></i> Add New Category</button>
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
                          Category
                      </th>
             
                   
                      <th  class="text-right">
                          Action
                      </th>
            
                      <th style="width: 10%" class="text-right">
                          Delete
                      </th>
                  </tr>
              </thead>
              <tbody>
              <%foreach (string category in categoryList)
                  {
                      string cat = Common.GetLastDir(category);%>
                  <tr>
                        <td>#</td>
                      <td><a   href="category_detail.aspx?cat=<%=cat%>"  ><%=cat %>  </a></td>

                       <td class="project-actions text-right">
                          <a class="btn btn-primary btn-sm copy-btn" href="#"  data-toggle="modal" data-target="#NewCategoryModal"  data-category="<%=cat%>">
                              <i class="fas fa-folder" >
                              </i>
                              Copy
                          </a>
                
                            <a class="btn btn-warning btn-sm delete-btn"  href="category_detail.aspx?cat=<%=cat%>"  >
                              <i class="fa fa-align-justify">
                              </i>
                              Detail
                          </a>
                           </td>
                      <td class="text-right">
                           <%if (!undeleteCategoryList.Contains(cat)){ %>
                          <a class="btn btn-danger btn-sm delete-btn "  href="#" data-toggle="modal" data-target="#DeleteCategoryModal" data-category="<%=cat%>">
                              <i class="fas fa-trash">
                              </i>
                              Delete
                          </a>
                           <%} %>
                      </td>
                  </tr>

                        
                  <%} %>
          
              </tbody>
          </table>

        </div>
        <!-- /.card-body -->
      </div>
      <!-- /.card -->

    </section>
    <!-- /.content -->
  
      <form action="category.aspx" method="post" id='NewCategoryForm'>
             <div class="modal fade" id="NewCategoryModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                 <div class="modal-dialog" role="document">
                     <div class="modal-content">
                         <div class="modal-header">
                             <h5 class="modal-title" id="NewCategoryHeader">New Category</h5>
                             <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                 <span aria-hidden="true">&times;</span>
                             </button>
                         </div>
                         <div class="modal-body">
                             
                             <div class="form-group row">
                                 <label for="recipient-name" class="col-form-label col-sm-4">New Category:</label>
                                 <input type="text" class="form-control  col-sm-8" name='category' >
                             </div>
                          <input type="hidden" id="copy_source_category" class="form-control  col-sm-10" name='copy_category'  />
                         </div>
                         <div class="modal-footer">
                             <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                             <button type="submit" id="NewCategorySubmit" name="cmd" class="btn btn-primary">Save</button>
                         </div>
                     </div>
                 </div>
             </div>
         </form>
   
      <form action="category.aspx" method="post" id='DeleteCategory'>
             <div class="modal fade" id="DeleteCategoryModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                 <div class="modal-dialog" role="document">
                     <div class="modal-content bg-danger">
                         <div class="modal-header">
                             <h5 class="modal-title" id="exampleModalLabel">Delete Category</h5>
                             <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                 <span aria-hidden="true">&times;</span>
                             </button>
                         </div>
                            <div class="modal-body" id="DeleteModalBody">
                             
                              
                            <%-- <input type="hidden" class="form-control  col-sm-10" name='company' id="s_new_sscat">--%>
                         </div>
                     
                         <div class="modal-footer">
                             <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                             <button type="submit" name='delete'  class="btn btn-primary" id="DeleteModalBtn">Delete</button>
                         </div>
                     </div>
                 </div>
             </div>
         </form>
</asp:Content>
<asp:Content ContentPlaceHolderID="AdditionalJS" runat="server">
   
    <script src="src/js/category.js"></script>
   
</asp:Content>
