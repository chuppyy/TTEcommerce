#!/bin/bash

# Array of categories with their actual IDs
declare -A categories=(
  ["12ua3nS2"]="Books & Stationery"
  ["1U8BtoWp"]="Home & Garden"
  ["4GwrjI7h"]="Electronics"
  ["83O4grIo"]="Health & Wellness"
  ["DgysGVBw"]="Jewelry & Watches"
  ["fldNCmS0"]="Fashion & Apparel"
  ["G1Kyvzco"]="Automotive"
  ["M89Xo4sh"]="New Category"
  ["mLRSDNLa"]="Toys & Games"
  ["p0XN1nwQ"]="Beauty & Personal Care"
  ["RWXcXPsO"]="Sports & Outdoors"
)

# Function to generate a random net weight
random_net_weight() {
  echo $(( (RANDOM % 1000) + 1 )).$(( RANDOM % 100 ))
}

# Loop through categories and create products
for id in "${!categories[@]}"
do
  category="${categories[$id]}"
  # Create 5 products for each category
  for i in {1..5}
  do
    name="Product ${i} for ${category}"
    description="This is product ${i} in the ${category} category"
    image_url="https://example.com/images/${category// /_}_${i}.jpg"
    net_weight=$(random_net_weight)
    
    product='{
      "name": "'"${name}"'",
      "description": "'"${description}"'",
      "imageUrl": "'"${image_url}"'",
      "netWeight": '"${net_weight}"',
      "categoryId": "'"${id}"'"
    }'
    
    curl -X POST "http://localhost:6000/api/product" \
         -H "Content-Type: application/json" \
         -d "$product"
    echo -e "\n"
  done
done
