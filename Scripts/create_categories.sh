#!/bin/bash

# Array of categories
categories=(
  '{"id": "electronics", "name": "Electronics", "description": "Gadgets, devices, and tech accessories"}'
  '{"id": "home-garden", "name": "Home & Garden", "description": "Furniture, decor, and gardening supplies"}'
  '{"id": "fashion-apparel", "name": "Fashion & Apparel", "description": "Clothing, shoes, and accessories"}'
  '{"id": "beauty-personal-care", "name": "Beauty & Personal Care", "description": "Cosmetics, skincare, and grooming products"}'
  '{"id": "sports-outdoors", "name": "Sports & Outdoors", "description": "Athletic equipment and outdoor gear"}'
  '{"id": "books-stationery", "name": "Books & Stationery", "description": "Books, office supplies, and writing materials"}'
  '{"id": "toys-games", "name": "Toys & Games", "description": "Playthings for all ages"}'
  '{"id": "automotive", "name": "Automotive", "description": "Car parts, accessories, and maintenance items"}'
  '{"id": "health-wellness", "name": "Health & Wellness", "description": "Vitamins, supplements, and fitness equipment"}'
  '{"id": "jewelry-watches", "name": "Jewelry & Watches", "description": "Fine jewelry, fashion jewelry, and timepieces"}'
)

# Loop through the categories and send POST requests
for category in "${categories[@]}"
do
  curl -X POST "http://localhost:6000/api/category" \
       -H "Content-Type: application/json" \
       -d "$category"
  echo -e "\n"
done
