using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary_Game__App.Tables
{
    public class insertData
    {

        public async Task<bool > insertHighScore(string nam, int score )
        {
            int lowpoint = score;
            string lowName = nam;
            int id = -1;
            //int count = 0;
            bool isAdded = false;

            var AllTerm = await App.conn.QueryAsync<tblHighScore>("SELECT * FROM tblHighScore");
            if (AllTerm != null)
            {
                foreach (var objT in AllTerm)
                {
                 if( objT.score < lowpoint)
                 {
                     lowName = objT.name;
                     lowpoint = objT.score;
                     id = objT.ID;
                     isAdded = true;
                 }
              
                }
            }
       
            if (isAdded == true)
            {


                 var user = await App.conn.Table<tblHighScore>().Where(x => x.ID == id ).FirstOrDefaultAsync();
                  if (user != null)
                  {
                      // Modify user
                      user.score = score;
                      user.name = nam;

                      // Update record
                      await App.conn.UpdateAsync(user);
                  }


            }


            return isAdded;
        }


        private void deleteScore(int id)
        {




        }

    }
}
