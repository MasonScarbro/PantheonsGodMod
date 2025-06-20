import os
import re

# Set to your target directory (defaults to current)
TARGET_DIR = "."

# Match SA.identifier -> "identifier"
pattern = re.compile(r'\bSA\.(\w+)\b')

def replace_in_file(filepath):
    with open(filepath, 'r', encoding='utf-8') as file:
        content = file.read()

    new_content, count = pattern.subn(r'"\1"', content)

    if count > 0:
        print(f"Replaced {count} occurrence(s) in {filepath}")
        with open(filepath, 'w', encoding='utf-8') as file:
            file.write(new_content)

def process_directory(path):
    for root, _, files in os.walk(path):
        for file in files:
            if file.endswith(('.cs', '.cpp', '.h', '.js', '.txt')):  # Add more extensions if needed
                full_path = os.path.join(root, file)
                replace_in_file(full_path)

if __name__ == "__main__":
    process_directory(TARGET_DIR)
