import psycopg2
import json


with open("MOCK_DATA.json", "r") as f:
    stock_data = json.load(f)


conn = psycopg2.connect(
    host="localhost",
    port=5432,
    dbname="StockMarket",
    user="postgres",
    password="password123"
)

cur = conn.cursor()


for stock in stock_data:
    cur.execute("""
        INSERT INTO "Stocks" (
            "StockSymbol",
            "StockName",
            "StockSector",
            "StockIndustry",
            "StockMarket",
            "StockCompanyName",
            "StockPrice",
            "MarketCap"
        ) VALUES (%s, %s, %s, %s, %s, %s, %s, %s)
    """, (
        stock["stock_symbol"],
        stock["stock_name"],
        stock["stock_sector"],
        stock["stock_industry"],
        stock["stock_market"],
        stock["stock_company_name"],
        stock["stock_price"],
        stock["market_cap"]
    ))

conn.commit()
print("Successfully seeded stock data from JSON.")

cur.close()
conn.close()
