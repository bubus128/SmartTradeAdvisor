CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE "MarketIndexes" (
    "Id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "Name" VARCHAR(255) NOT NULL,
    "Description" VARCHAR(255) NOT NULL,
    "IsSymbol" BOOLEAN NOT NULL
);

CREATE TABLE "MarketIndexValues" (
    "Id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "Date" TIMESTAMP NOT NULL,
    "Value" DECIMAL NOT NULL,
    "IndexId" UUID NOT NULL,
    FOREIGN KEY ("IndexId") REFERENCES "MarketIndexes"("Id")
);