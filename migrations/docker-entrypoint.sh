#!/bin/sh -x

#!/bin/sh

liquibaseUpdate() {
#  liquibase update --changeLogFile=$LIQUIBASE_COMMAND_CHANGELOG_FILE --url=$LIQUIBASE_COMMAND_URL --username=$PG_USERNAME --password=$PG_PASSWORD --driver=$LIQUIBASE_COMMAND_DRIVER

liquibase update --changeLogFile=changelog.xml --url=jdbc:postgresql://${PG_HOST}:5432/${PG_DATABASE} --username=${PG_USERNAME} --password=${PG_PASSWORD} --driver=org.postgresql.Driver

}

sortDirectory() {
  cd scripts
  for dir in *; do
    echo "$dir" >>../unsorted.txt
  done
  sort -V ../unsorted.txt >../sorted.txt
  rm ../unsorted.txt
}

readScripts() {
  cat ../sorted.txt | while read line || [[ -n $line ]]; do
    echo "$line"

    for filename in "$line"/*; do
      echo "$filename"  >> ../files.txt
    done
  done
  rm ../sorted.txt
}

generateXml(){
  {
  echo '<?xml version="1.0" encoding="UTF-8"?>'
  echo '<databaseChangeLog
        xmlns="http://www.liquibase.org/xml/ns/dbchangelog"
        xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
        xmlns:pro="http://www.liquibase.org/xml/ns/pro"
        xsi:schemaLocation="http://www.liquibase.org/xml/ns/dbchangelog
    http://www.liquibase.org/xml/ns/dbchangelog/dbchangelog-4.1.xsd
    http://www.liquibase.org/xml/ns/pro
    http://www.liquibase.org/xml/ns/pro/liquibase-pro-4.1.xsd">'
  cat ../files.txt | while read line || [[ -n $line ]]; do
    echo '<include file="scripts/'"$line"'" relativeToChangelogFile="true"/>'
  done
  echo '</databaseChangeLog>'

} > ../changelog.xml
rm ../files.txt
cd ..
}
sortDirectory
readScripts
generateXml
liquibaseUpdate

if [ "$?" != 0 ]; then
  echo "DB Migration failed"
  #exit 1
fi
exec $@
