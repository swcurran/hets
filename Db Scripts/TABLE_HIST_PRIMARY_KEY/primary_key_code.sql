-- Constraint: HET_EQUIPMENT_HIST_PK

ALTER TABLE public."HET_EQUIPMENT_HIST" DROP CONSTRAINT IF EXISTS "HET_EQUIPMENT_HIST_PK";

ALTER TABLE public."HET_EQUIPMENT_HIST"
    ADD CONSTRAINT "HET_EQUIPMENT_HIST_PK" PRIMARY KEY ("EQUIPMENT_HIST_ID");



-- Constraint: HET_EQUIPMENT_ATTACHMENT_HIST_PK

ALTER TABLE public."HET_EQUIPMENT_ATTACHMENT_HIST" DROP CONSTRAINT IF EXISTS "HET_EQUIPMENT_ATTACHMENT_HIST_PK";

ALTER TABLE public."HET_EQUIPMENT_ATTACHMENT_HIST"
    ADD CONSTRAINT "HET_EQUIPMENT_ATTACHMENT_HIST_PK" PRIMARY KEY ("EQUIPMENT_ATTACHMENT_HIST_ID");



-- Constraint: HET_NOTE_HIST_PK

ALTER TABLE public."HET_NOTE_HIST" DROP CONSTRAINT IF EXISTS "HET_NOTE_HIST_PK";

ALTER TABLE public."HET_NOTE_HIST"
    ADD CONSTRAINT "HET_NOTE_HIST_PK" PRIMARY KEY ("NOTE_HIST_ID");    



-- Constraint: HET_RENTAL_AGREEMENT_HIST_PK

ALTER TABLE public."HET_RENTAL_AGREEMENT_HIST" DROP CONSTRAINT IF EXISTS "HET_RENTAL_AGREEMENT_HIST_PK";

ALTER TABLE public."HET_RENTAL_AGREEMENT_HIST"
    ADD CONSTRAINT "HET_RENTAL_AGREEMENT_HIST_PK" PRIMARY KEY ("RENTAL_AGREEMENT_HIST_ID");



-- Constraint: HET_RENTAL_AGREEMENT_CONDITION_HIST_PK

ALTER TABLE public."HET_RENTAL_AGREEMENT_CONDITION_HIST" DROP CONSTRAINT IF EXISTS "HET_RENTAL_AGREEMENT_CONDITION_HIST_PK";

ALTER TABLE public."HET_RENTAL_AGREEMENT_CONDITION_HIST"
    ADD CONSTRAINT "HET_RENTAL_AGREEMENT_CONDITION_HIST_PK" PRIMARY KEY ("RENTAL_AGREEMENT_CONDITION_HIST_ID");



-- Constraint: HET_RENTAL_AGREEMENT_RATE_HIST_PK

ALTER TABLE public."HET_RENTAL_AGREEMENT_RATE_HIST" DROP CONSTRAINT IF EXISTS "HET_RENTAL_AGREEMENT_RATE_HIST_PK";

ALTER TABLE public."HET_RENTAL_AGREEMENT_RATE_HIST"
    ADD CONSTRAINT "HET_RENTAL_AGREEMENT_RATE_HIST_PK" PRIMARY KEY ("RENTAL_AGREEMENT_RATE_HIST_ID");



-- Constraint: HET_RENTAL_REQUEST_ROTATION_LIST_HIST_PK

ALTER TABLE public."HET_RENTAL_REQUEST_ROTATION_LIST_HIST" DROP CONSTRAINT IF EXISTS "HET_RENTAL_REQUEST_ROTATION_LIST_HIST_PK";

ALTER TABLE public."HET_RENTAL_REQUEST_ROTATION_LIST_HIST"
    ADD CONSTRAINT "HET_RENTAL_REQUEST_ROTATION_LIST_HIST_PK" PRIMARY KEY ("RENTAL_REQUEST_ROTATION_LIST_HIST_ID");



-- Constraint: HET_TIME_RECORD_HIST_PK

ALTER TABLE public."HET_TIME_RECORD_HIST" DROP CONSTRAINT IF EXISTS "HET_TIME_RECORD_HIST_PK";

ALTER TABLE public."HET_TIME_RECORD_HIST"
    ADD CONSTRAINT "HET_TIME_RECORD_HIST_PK" PRIMARY KEY ("TIME_RECORD_HIST_ID");
