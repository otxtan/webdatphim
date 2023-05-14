# monquantriduan
b1: cài đặt visual studio , sql server </br>
b2: mở visual studio-> new project paste link git repo vào và clone </br>
b3: mở file datphim.sln trong solution explorer</br>
b4: mở sql server-> kết nối -> right click -> backup database</br>
b5: thiết lập đường dẫn kết nối database trong file web.cofig tìm đến</br>
  <add name="datphimchuanEntities" connectionString="metadata=res://*/Models.Model2.csdl|res://*/Models.Model2.ssdl|res://*/Models.Model2.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=$;initial catalog=datphimchuan1;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
  thay đổi datasoure = tên server name trong sql server </br>
b6: quay lại visual studio ctrl + f5 để chạy chế độ không debug</br>
b7: đăng nhập tài khoản mặc đinh của quyền admin tk: admin mk: Admin123456@, user: tk: otxtan12 mk:Admin123456@ </br>

the end!
