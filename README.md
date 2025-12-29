# 🍄 **ShroomDB**  
### *A tiny, single‑file, segment‑based storage engine that grows on you.*

Welcome to **ShroomDB**, the storage engine inspired by nature’s most underrated engineers: mushrooms.

Just like a real mushroom is only the tip of a vast, efficient underground network, ShroomDB stores your data in a compact, linked, page‑based structure that’s small, flexible, and surprisingly powerful.

It’s not a database.  
It’s not a filesystem.  
It’s… something in between.  
A **mycelial storage layer** for thousands of tiny, independent data blobs.

---

## 🌱 **What is ShroomDB?**

ShroomDB is a **single‑file, variable‑segment storage engine** designed for:

- many small independent records (“tokens”)
- variable‑length data
- compactness above all else
- predictable performance
- deterministic layout (easy to hash, audit, replicate)
- embedded use cases
- fun

It’s built around three simple ideas:

1. **Tokens**  
   Each token has its own mini‑storage, like a tiny mushroom cap.

2. **Segments**  
   Data is stored in small variable‑length chunks linked together.

3. **Pages**  
   The file is divided into pages, each with minimal metadata and maximum usable space.

Everything lives in one file.  
Everything is deterministic.  
Everything is tiny and efficient.

---

## 🍄 **Why “ShroomDB”?**

Because mushrooms are:

- compact  
- efficient  
- modular  
- self‑organizing  
- surprisingly powerful  
- and honestly… fun  

Also, “ShroomDB” is way more memorable than “Segmented Variable‑Length Multi‑Page Storage Engine v0.1”.

---

## 🧱 **Core Concepts**

### **📦 Tokens**
A token is a logical storage unit.  
Think of it as a mini‑file inside the big file.

Each token has:

- an ID  
- total length  
- first page + offset  
- number of pages used  

Tokens are independent — no global schema, no shared structure.

---

### **🧩 Segments**
Data is stored in **segments**:

```
[Length: 2 bytes]
[Data: N bytes]
[NextOffset: 2 bytes]
```

Segments link together to form a chain.  
This allows:

- variable‑length data  
- efficient small writes  
- minimal overhead  
- flexible growth  

---

### **📄 Pages**
The file is divided into pages (up to ~65 KB each).

Each page has **minimal metadata**:

- PageType (index / storage / free)
- UsedSize
- Reserved (future checksum or ID)

Everything else is raw storage.

---

### **🗑 Free Space Index**
When data is deleted, its segments are added to the **free space index**.

This index is the allocator:

- finds space for new writes  
- tracks fragmentation  
- enables compaction  
- lives inside the same file  

No wasted space.  
No external metadata.  
No magic.

---

## 🚀 **Features**

- **Single‑file storage**  
  Easy to hash, copy, replicate, or embed.

- **Tiny overhead**  
  Segments are compact, pages are compact, everything is compact.

- **Independent variable‑size records**  
  Perfect for metadata, small objects, configs, flags, counters, etc.

- **Deterministic layout**  
  Great for auditability and cryptographic hashing.

- **Free‑space reuse**  
  Efficient allocation without fragmentation explosions.

- **Friendly API**  
  (Coming soon)

---

## 🧪 **What ShroomDB is NOT**

- Not a SQL database  
- Not a key‑value store  
- Not a document store  
- Not a filesystem  
- Not a mushroom‑themed blockchain (yet)

It’s a **storage engine** — the layer *under* all those things.

---

## 🛠 **Use Cases**

- Embedded systems  
- Game engines  
- Blockchain-like state storage  
- Config/state blobs  
- Metadata stores  
- Anything needing thousands of tiny independent storages  

---

## 📚 **Example (coming soon)**

```csharp
var db = ShroomDB.Open("state.shroom");

db.Put(tokenId, data);
var bytes = db.Get(tokenId);
db.Delete(tokenId);

db.Flush();
```

---

## 🧬 **Roadmap**

- [ ] v0.1 — Core engine (pages, segments, index, free list)
- [ ] v0.2 — Compaction
- [ ] v0.3 — Streaming writes
- [ ] v0.4 — Snapshots
- [ ] v0.5 — Hashing the entire file
- [ ] v1.0 — Stable API + docs

---

## 🍄 **Contributing**

Pull requests welcome.  
Bug reports welcome.  
Mushroom puns encouraged.

---

## 🧠 **License**

MIT — because fungi belong to everyone.
