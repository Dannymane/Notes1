
// Efficiently insert a large number of records into a SQLite database
export class ArticleRepositoryService {
	private chunkSizeToInsert: number = 5000;

	constructor(private context: ContextService) {}

	async addArticles(articles: Article[]) {
		for (let index = 0; index < articles.length; index += this.chunkSizeToInsert) {
			let articlesToAdd = articles.slice(index, index + this.chunkSizeToInsert);
			let sql =
				`INSERT INTO "` +
				TableName[TableName.Article] +
				`" (id, articleNumber, ean, name, price, unitId, groupId, archiveStatus, additionalInfo, isActive) VALUES `;
			let values: any[] = [];

			articlesToAdd.forEach((article) => {
				sql += `(?, ?, ?, ?, ?, ?, ?, ?, ?, ?),`;
				values.push(
					article.id,
					article.articleNumber,
					article.ean,
					article.name,
					article.price,
					article.unitId,
					article.groupId,
					article.archiveStatus,
					JSON.stringify(article.additionalInfo),
					article.isActive
				);
			});
			let cmd = sql.slice(0, -1);
			cmd += ';';
			await this.context.db.run(cmd, values);
		}
	}
// Efficient way to find an article by EAN
	async findArticleByEan(ean: string): Promise<boolean | SimpleArticleDto> {
		const articles: SimpleArticleDto[] = (
			await this.context.db.query(
				`SELECT a.id, a.ean, a.name, u.displayName as unitName, archiveStatus
				FROM "${TableName[TableName.Article]}" a 
				LEFT OUTER JOIN "${TableName[TableName.Unit]}" u 
				ON a.unitId = u.Id 
				WHERE a.ean = '${ean}' 
				LIMIT 1;`
			)
		).values as SimpleArticleDto[];
		if (articles.length > 0) {
			return articles[0];
		}
		return false;
	}
}