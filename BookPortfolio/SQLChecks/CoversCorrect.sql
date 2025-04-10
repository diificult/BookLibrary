SELECT Id, coverIds
FROM Books
WHERE ISJSON(coverIds) = 0 AND coverIds IS NOT NULL;