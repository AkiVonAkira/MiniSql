ALTER TABLE "public"."ika_project_person" DROP CONSTRAINT "FK_ika_person_project_person_id";
ALTER TABLE "public"."ika_project_person" DROP CONSTRAINT "FK_ika_project_person_project_id";
DROP TABLE "public"."ika_person";
DROP TABLE "public"."ika_project";
DROP TABLE "public"."ika_project_person";
CREATE TABLE "public"."ika_person" ( 
  "id" SERIAL,
  "person_name" VARCHAR(25) NOT NULL,
  CONSTRAINT "ika_person_pkey" PRIMARY KEY ("id")
);
CREATE TABLE "public"."ika_project" ( 
  "id" SERIAL,
  "project_name" VARCHAR(50) NOT NULL,
  CONSTRAINT "ika_project_pkey" PRIMARY KEY ("id")
);
CREATE TABLE "public"."ika_project_person" ( 
  "id" SERIAL,
  "project_id" INTEGER NOT NULL,
  "person_id" INTEGER NOT NULL,
  "hours" INTEGER NOT NULL,
  CONSTRAINT "ika_project_person_pkey" PRIMARY KEY ("id")
);
TRUNCATE TABLE "public"."ika_person";
TRUNCATE TABLE "public"."ika_project";
TRUNCATE TABLE "public"."ika_project_person";
ALTER TABLE "public"."ika_project_person" ADD CONSTRAINT "FK_ika_project_person_project_id" FOREIGN KEY ("project_id") REFERENCES "public"."ika_project" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "public"."ika_project_person" ADD CONSTRAINT "FK_ika_person_project_person_id" FOREIGN KEY ("person_id") REFERENCES "public"."ika_person" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
