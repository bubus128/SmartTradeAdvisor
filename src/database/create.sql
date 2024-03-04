-- Tworzenie tabeli na symbole giełdowe
CREATE TABLE symbols (
    id SERIAL PRIMARY KEY,
    symbol VARCHAR(10) UNIQUE NOT NULL
);

-- Tworzenie tabeli na ceny historyczne
CREATE TABLE historical_prices (
    id SERIAL PRIMARY KEY,
    symbol_id INT,
    price DECIMAL(10, 2) NOT NULL,
    date DATE NOT NULL,
    FOREIGN KEY (symbol_id) REFERENCES symbols(id),
    UNIQUE (symbol_id, date)
);

-- Tworzenie tabeli na wskaźniki
CREATE TABLE indicators (
    id SERIAL PRIMARY KEY,
    symbol_id INT,
    indicator_name VARCHAR(50) NOT NULL,
    value DECIMAL(10, 4) NOT NULL,
    date DATE NOT NULL,
    FOREIGN KEY (symbol_id) REFERENCES symbols(id),
    FOREIGN KEY (symbol_id, date) REFERENCES historical_prices(symbol_id, date),
    UNIQUE (symbol_id, indicator_name, date)
);