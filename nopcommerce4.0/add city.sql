﻿
--
-- Table structure for table `sa_cities`
--
drop table city
CREATE TABLE City (
  id int NOT NULL,
  nameAr NVARCHAR(64)  COLLATE Arabic_CI_AI_KS NOT NULL,
  nameEn varchar(32) NOT NULL,
  provinceId int NOT NULL
) ;

--
-- Dumping data for table `sa_cities`
--

INSERT INTO City (id, nameAr, nameEn, provinceId) VALUES
(1, N'تبوك', N'Tabuk', 3),
(3, N'الرياض', N'Riyadh', 6),
(5, N'الطائف', N'At Taif', 11),
(6, N'مكة المكرمة', N'Makkah Al Mukarramah', 11),
(10, N'حائل', N'Hail', 4),
(11, N'بريدة', N'Buraydah', 5),
(12, N'الهفوف', N'Al Hufuf', 13),
(13, N'الدمام', N'Ad Dammam', 13),
(14, N'المدينة المنورة', N'Al Madinah Al Munawwarah', 7),
(15, N'ابها', N'Abha', 8),
(17, N'جازان', N'Jazan', 10),
(18, N'جدة', N'Jeddah', 11),
(24, N'المجمعة', N'Al Majmaah', 6),
(31, N'الخبر', N'Al Khubar', 13),
(47, N'حفر الباطن', N'Hafar Al Batin', 13),
(62, N'خميس مشيط', N'Khamis Mushayt', 8),
(65, N'احد رفيده', N'Ahad Rifaydah', 8),
(67, N'القطيف', N'Al Qatif', 13),
(80, N'عنيزة', N'Unayzah', 5),
(89, N'قرية العليا', N'Qaryat Al Ulya', 13),
(113, N'الجبيل', N'Al Jubail', 13),
(115, N'النعيرية', N'An Nuayriyah', 13),
(227, N'الظهران', N'Dhahran', 13),
(233, N'الوجه', N'Al Wajh', 3),
(243, N'بقيق', N'Buqayq', 13),
(270, N'الزلفي', N'Az Zulfi', 6),
(288, N'خيبر', N'Khaybar', 7),
(306, N'الغاط', N'Al Ghat', 6),
(323, N'املج', N'Umluj', 3),
(377, N'رابغ', N'Rabigh', 11),
(418, N'عفيف', N'Afif', 6),
(443, N'ثادق', N'Thadiq', 6),
(454, N'سيهات', N'Sayhat', 13),
(456, N'تاروت', N'Tarut', 13),
(483, N'ينبع', N'Yanbu', 7),
(500, N'شقراء', N'Shaqra', 6),
(669, N'الدوادمي', N'Ad Duwadimi', 6),
(828, N'الدرعية', N'Ad Diriyah', 6),
(880, N'القويعية', N'Quwayiyah', 6),
(990, N'المزاحمية', N'Al Muzahimiyah', 6),
(1053, N'بدر', N'Badr', 7),
(1061, N'الخرج', N'Al Kharj', 6),
(1073, N'الدلم', N'Ad Dilam', 6),
(1228, N'الشنان', N'Ash Shinan', 4),
(1248, N'الخرمة', N'Al Khurmah', 11),
(1257, N'الجموم', N'Al Jumum', 11),
(1294, N'المجاردة', N'Al Majardah', 8),
(1361, N'السليل', N'As Sulayyil', 6),
(1443, N'تثليث', N'Tathilith', 8),
(1514, N'بيشة', N'Bishah', 8),
(1542, N'الباحة', N'Al Baha', 9),
(1625, N'القنفذة', N'Al Qunfidhah', 11),
(1801, N'محايل', N'Muhayil', 8),
(1879, N'ثول', N'Thuwal', 11),
(1947, N'ضبا', N'Duba', 3),
(2156, N'تربه', N'Turbah', 11),
(2167, N'صفوى', N'Safwa', 13),
(2171, N'عنك', N'Inak', 13),
(2208, N'طريف', N'Turaif', 1),
(2213, N'عرعر', N'Arar', 1),
(2226, N'القريات', N'Al Qurayyat', 2),
(2237, N'سكاكا', N'Sakaka', 2),
(2256, N'رفحاء', N'Rafha', 1),
(2268, N'دومة الجندل', N'Dawmat Al Jandal', 2),
(2421, N'الرس', N'Ar Rass', 5),
(2448, N'المذنب', N'Al Midhnab', 5),
(2464, N'الخفجي', N'Al Khafji', 13),
(2467, N'رياض الخبراء', N'Riyad Al Khabra', 5),
(2481, N'البدائع', N'Al Badai', 5),
(2590, N'رأس تنورة', N'Ras Tannurah', 13),
(2630, N'البكيرية', N'Al Bukayriyah', 5),
(2777, N'الشماسية', N'Ash Shimasiyah', 5),
(3158, N'الحريق', N'Al Hariq', 6),
(3161, N'حوطة بني تميم', N'Hawtat Bani Tamim', 6),
(3174, N'ليلى', N'Layla', 6),
(3275, N'بللسمر', N'Billasmar', 8),
(3347, N'شرورة', N'Sharurah', 12),
(3417, N'نجران', N'Najran', 12),
(3479, N'صبيا', N'Sabya', 10),
(3525, N'ابو عريش', N'Abu Arish', 10),
(3542, N'صامطة', N'Samtah', 10),
(3652, N'احد المسارحة', N'Ahad Al Musarihah', 10),
(3666, N'مدينة الملك عبدالله الاقتصادية', N'King Abdullah Economic City', 11);

select * from City