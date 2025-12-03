import math

used_items = set()

def validIDp1(ID: str):
    ID_half = ID[:(len(ID) // 2)]
    if (int(ID_half) * (10 ** (len(ID_half)))) + int(ID_half) == int(ID):
        return False
    return True

def validIDp2(ID: str, length: int, high: int, low: int) -> int:
    total = 0

    segments = [(ID[:a], length // a) 
           for a in range(1, (len(ID)) + 1)
           if length % a == 0]
    for xs, n in segments:
        check = int(xs * n)
        if check >= low and check <= high and check not in used_items:
            total += check
            used_items.add(check)
    
    return total

def gift():
    total = 0

    with open("input.txt") as f:
        allIDs = f.read().split(',')

    for IDs in allIDs:
        used_items.clear()
        low, high = tuple(IDs.split('-'))
        
        low_seg = str(low)[:math.ceil(len(low) / 2)]
        high_seg = str(high)[:math.ceil((len(high) / 2))]
        for ID in range(int(low_seg), int(high_seg) + 1):
            total += validIDp2(str(ID), len(low), int(high), int(low))
    print(total)

gift()