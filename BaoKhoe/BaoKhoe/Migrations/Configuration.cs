using BaoKhoe.Models;

namespace BaoKhoe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AppDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(AppDBContext context)
        {
            if (context.Users.FirstOrDefault(x => x.Username.Equals("spider")) == null)
            {
                context.Users.Add(new User()
                {
                    FullName = "Spider",
                    Username = "spider",
                    Password = "spider",
                    CreatedAt = DateTime.Now,
                    Email = String.Empty,
                    Status = "active"
                });

                context.SaveChanges();
            }

            if (context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Tin Tuc")) == null)
            {
                context.Categories.Add(new Category()
                {
                    Name = "Tin Tức",
                    FriendlyName = "Tin Tuc",
                    Url = "tin-tuc",
                    Status = "active",
                    Descriptions = "Tin Tức",
                    IsSubCategory = false
                });
            }

            if (context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("An Toan Thuc Pham")) == null)
            {
                context.Categories.Add(new Category()
                {
                    Name = "An Toàn Thực Phẩm",
                    FriendlyName = "An Toan Thuc Pham",
                    Url = "an-toan-thuc-pham",
                    Status = "active",
                    Descriptions = "An Toàn Thực Phẩm",
                    IsSubCategory = false
                });
            }

            if (context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Phong Va Chua Benh")) == null)
            {
                context.Categories.Add(new Category()
                {
                    Name = "Phòng Và Chữa Bệnh",
                    FriendlyName = "Phong Va Chua Benh",
                    Url = "phong-va-chua-benh",
                    Status = "active",
                    Descriptions = "Phòng Và Chữa Bệnh",
                    IsSubCategory = false
                });
            }

            if (context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Song Khoe Moi Ngay")) == null)
            {
                context.Categories.Add(new Category()
                {
                    Name = "Sống Khỏe Mỗi Ngày",
                    FriendlyName = "Song Khoe Moi Ngay",
                    Url = "song-khoe-moi-ngay",
                    Status = "active",
                    Descriptions = "Sống Khỏe Mỗi Ngày",
                    IsSubCategory = false
                });
            }

            if (context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Lam Dep")) == null)
            {
                context.Categories.Add(new Category()
                {
                    Name = "Làm Đẹp",
                    FriendlyName = "Lam Dep",
                    Url = "lam-dep",
                    Status = "active",
                    Descriptions = "Làm Đẹp",
                    IsSubCategory = false
                });
            }

            if (context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Gioi Tinh")) == null)
            {
                context.Categories.Add(new Category()
                {
                    Name = "Giới Tính",
                    FriendlyName = "Gioi Tinh",
                    Url = "gioi-tinh",
                    Status = "active",
                    Descriptions = "Giới Tính",
                    IsSubCategory = false
                });
            }

            if (context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Me Va Be")) == null)
            {
                context.Categories.Add(new Category()
                {
                    Name = "Mẹ Và Bé",
                    FriendlyName = "Me Va Be",
                    Url = "me-va-be",
                    Status = "active",
                    Descriptions = "Mẹ Và Bé",
                    IsSubCategory = false
                });
            }

            if (context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Y Hoc Co Truyen")) == null)
            {
                context.Categories.Add(new Category()
                {
                    Name = "Y Học Cổ Truyền",
                    FriendlyName = "Y Hoc Co Truyen",
                    Url = "y-hoc-co-truyen",
                    Status = "active",
                    Descriptions = "Y Học Cổ Truyền",
                    IsSubCategory = false
                });
            }

            if (context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Goc Chuyen Gia")) == null)
            {
                context.Categories.Add(new Category()
                {
                    Name = "Góc Chuyên Gia",
                    FriendlyName = "Goc Chuyen Gia",
                    Url = "goc-chuyen-gia",
                    Status = "active",
                    Descriptions = "Góc Chuyên Gia",
                    IsSubCategory = false
                });
            }

            if (context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Tien Bo Y Hoc")) == null)
            {
                context.Categories.Add(new Category()
                {
                    Name = "Tiến Bộ Y Học",
                    FriendlyName = "Tien Bo Y Hoc",
                    Url = "tien-bo-y-hoc",
                    Status = "active",
                    Descriptions = "Tiến Bộ Y Học",
                    IsSubCategory = true
                });
            }

            if (context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Benh Thuong Gap")) == null)
            {
                context.Categories.Add(new Category()
                {
                    Name = "Bệnh Thường Gặp",
                    FriendlyName = "Benh Thuong Gap",
                    Url = "benh-thuong-gap",
                    Status = "active",
                    Descriptions = "Bệnh Thường Gặp",
                    IsSubCategory = true
                });
            }

            if (context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Benh Nguoi Cao Tuoi")) == null)
            {
                context.Categories.Add(new Category()
                {
                    Name = "Bệnh Người Cao Tuổi",
                    FriendlyName = "Benh Nguoi Cao Tuoi",
                    Url = "benh-nguoi-cao-tuoi",
                    Status = "active",
                    Descriptions = "Bệnh Người Cao Tuổi",
                    IsSubCategory = true
                });
            }

            if (context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Dinh Duong")) == null)
            {
                context.Categories.Add(new Category()
                {
                    Name = "Dinh Dưỡng",
                    FriendlyName = "Dinh Duong",
                    Url = "dinh-duong",
                    Status = "active",
                    Descriptions = "Dinh Dưỡng",
                    IsSubCategory = true
                });
            }

            if (context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("My Pham")) == null)
            {
                context.Categories.Add(new Category()
                {
                    Name = "Mỹ Phẩm",
                    FriendlyName = "My Pham",
                    Url = "my-pham",
                    Status = "active",
                    Descriptions = "Mỹ Phẩm",
                    IsSubCategory = true
                });
            }

            if (context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Tham My")) == null)
            {
                context.Categories.Add(new Category()
                {
                    Name = "Thẩm Mỹ",
                    FriendlyName = "Tham My",
                    Url = "tham-my",
                    Status = "active",
                    Descriptions = "Thẩm Mỹ",
                    IsSubCategory = true
                });
            }

            if (context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Day Tre")) == null)
            {
                context.Categories.Add(new Category()
                {
                    Name = "Dậy Trẻ",
                    FriendlyName = "Day Tre",
                    Url = "day-tre",
                    Status = "active",
                    Descriptions = "Dậy Trẻ",
                    IsSubCategory = true
                });
            }

            if (context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("San Phu Khoa")) == null)
            {
                context.Categories.Add(new Category()
                {
                    Name = "Sản Phụ Khoa",
                    FriendlyName = "San Phu Khoa",
                    Url = "san-phu-khoa",
                    Status = "active",
                    Descriptions = "Sản Phụ Khoa",
                    IsSubCategory = true
                });
            }

            if (context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Benh Di Truyen")) == null)
            {
                context.Categories.Add(new Category()
                {
                    Name = "Bệnh Di Truyền",
                    FriendlyName = "Benh Di Truyen",
                    Url = "benh-di-truyen",
                    Status = "active",
                    Descriptions = "Bệnh Di Truyền",
                    IsSubCategory = true
                });
            }

            if (context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Tam Su Tham Kin")) == null)
            {
                context.Categories.Add(new Category()
                {
                    Name = "Tâm Sự Thầm Kín",
                    FriendlyName = "Tam Su Tham Kin",
                    Url = "tam-su-tham-kin",
                    Status = "active",
                    Descriptions = "Tâm Sự Thầm Kín",
                    IsSubCategory = true
                });
            }

            if (context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Chuyen Phong The")) == null)
            {
                context.Categories.Add(new Category()
                {
                    Name = "Chuyện Phòng The",
                    FriendlyName = "Chuyen Phong The",
                    Url = "chuyen-phong-the",
                    Status = "active",
                    Descriptions = "Chuyện Phòng The",
                    IsSubCategory = true
                });
            }

            if (context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Benh Lay Nhiem")) == null)
            {
                context.Categories.Add(new Category()
                {
                    Name = "Bệnh Lây Nhiễm",
                    FriendlyName = "Benh Lay Nhiem",
                    Url = "benh-lay-nhiem",
                    Status = "active",
                    Descriptions = "Bệnh Lây Nhiễm",
                    IsSubCategory = true
                });
            }

            if (context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Bai Thuoc Dan Gian")) == null)
            {
                context.Categories.Add(new Category()
                {
                    Name = "Bài Thuốc Dân Gian",
                    FriendlyName = "Bai Thuoc Dan Gian",
                    Url = "bai-thuoc-dan-gian",
                    Status = "active",
                    Descriptions = "Bài Thuốc Dân Gian",
                    IsSubCategory = true
                });
            }

            if (context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Cây Thuoc Quanh Ta")) == null)
            {
                context.Categories.Add(new Category()
                {
                    Name = "Cây Thuốc Quanh Ta",
                    FriendlyName = "Cay Thuoc Quanh Ta",
                    Url = "cay-thuoc-quanh-ta",
                    Status = "active",
                    Descriptions = "Cây Thuốc Quanh Ta",
                    IsSubCategory = true
                });
            }

            context.SaveChanges();

            Category skmn = context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Song Khoe Moi Ngay"));
            skmn?.SubCategories.Add(context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Dinh Duong")));

            Category tt = context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Tin Tuc"));
            tt?.SubCategories.Add(context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Tien Bo Y Hoc")));

            Category yhct = context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Y Hoc Co Truyen"));
            yhct?.SubCategories.Add(context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Bai Thuoc Dan Gian")));
            yhct?.SubCategories.Add(context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Cay Thuoc Quanh Ta")));
            
            Category b = context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Phong Va Chua Benh"));
            b?.SubCategories.Add(context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Benh Thuong Gap")));
            b?.SubCategories.Add(context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Benh Nguoi Cao Tuoi")));

            Category gt = context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Gioi Tinh"));
            gt?.SubCategories.Add(context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Tam Su Tham Kin")));
            gt?.SubCategories.Add(context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Chuyen Phong The")));
            gt?.SubCategories.Add(context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Benh Lay Nhiem")));

            Category mvb = context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Me Va Be"));
            mvb?.SubCategories.Add(context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Day Tre")));
            mvb?.SubCategories.Add(context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("San Phu Khoa")));
            mvb?.SubCategories.Add(context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Benh Di Truyen")));

            Category kd = context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Lam Dep"));
            kd?.SubCategories.Add(context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("My Pham")));
            kd?.SubCategories.Add(context.Categories.FirstOrDefault(x => x.FriendlyName.Equals("Tham My")));

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
