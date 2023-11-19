BEGIN TRANSACTION;
INSERT INTO "TagType" ("Id","TypeName") VALUES (1,'series'),
 (2,'character'),
 (3,'filetype'),
 (4,'mediatype');
INSERT INTO "MediaTable" ("Id","Location","Rating") VALUES (1,'D:\Anime pictures\__hololive__\__amane_kanata_and_tokoyami_towa_hololive_drawn_by_nootomo__8699225b4bcf8d6431bf7c4a234e4aea.jpg',NULL);
INSERT INTO "TagTable" ("Id","Tag","TagTypeId") VALUES (1,'amane kanata',2),
 (2,'hololive',1);
INSERT INTO "TagToImage" ("Id","TagId","MediaId") VALUES (1,1,1),
 (2,2,1);
COMMIT;
